using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Pick_One.Input
{
    public class KeyboardListener
    {

        private KeyboardChangeState KeyboardStates { get; set; }
        private List<KeyboardSubscriber> Subscribers { get; set; }
        private double elapsedTime = 0;
        private static readonly int MILLISECONDS_PER_SECOND = 1000;
        private static readonly int UPDATES_PER_SECOND = 70;
        private int timeSpanMilliseconds = MILLISECONDS_PER_SECOND / UPDATES_PER_SECOND;


        public KeyboardListener()
        {
            KeyboardStates = new KeyboardChangeState();
            Subscribers = new List<KeyboardSubscriber>();

        }

        public void Update(KeyboardState currentState, GameTime gameTime)
        {
            //Dan can you check that this is not being called tomany times?
            elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;

            KeyboardStates.SetState(currentState);

            if ((int)elapsedTime >= TimeSpan.FromMilliseconds(10).TotalMilliseconds)
            {
                notifySubscribers(gameTime);
            }

            if (elapsedTime > 1000) { elapsedTime = 0; }
        }

        public void AddSubscriber(KeyboardSubscriber subscriber)
        {
            if (subscriber != null && !Subscribers.Contains(subscriber))
            {
                Subscribers.Add(subscriber);
            }
        }
        public void RemoveSubscriber(KeyboardSubscriber subscriber)
        {
            Subscribers.Remove(subscriber);
        }
        public void notifySubscribers(GameTime gameTime)
        {
            KeyboardSubscriber[] tempSubscribers = new KeyboardSubscriber[Subscribers.Count];
            Subscribers.CopyTo(tempSubscribers);
            List<KeyAction> actions;
            foreach (var subscriber in tempSubscribers)
            {
                if (!subscriber.IsPaused)//checks that Listening is not paused on this subscriber
                {
                    actions = new List<KeyAction>();
                    foreach (var key in subscriber.WatchedKeys)
                    {
                        var tempKey = new KeyAction(key);
                        tempKey.Key = key;
                        if (KeyboardStates.WasKeyPressed(key))
                        {
                            tempKey.WasPressed = true;
                        }

                        if (KeyboardStates.WasKeyReleased(key))
                        {
                            tempKey.WasReleased = true;
                        }

                        if (KeyboardStates.IsKeyBeingHeld(key))
                        {
                            tempKey.IsBeingHeld = true;
                        }
                        actions.Add(tempKey);
                    }
                    subscriber.Subscriber.NotifyOfChange(actions, gameTime);
                }

                
            }

        }

    }
}

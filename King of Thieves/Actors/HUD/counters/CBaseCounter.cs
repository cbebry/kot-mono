﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Actors.HUD.counters
{
    class CBaseCounter : CHUDElement
    {
        private static SpriteFont _sherwood = CMasterControl.glblContent.Load<SpriteFont>(@"Fonts/sherwood");
        private int _capacity;
        private int _amount;
        private int _incrementAmount = 0;
        private bool _instantaneousUpdate = false;
        private Vector2 _textOffset = Vector2.Zero;
        private Color _textColor = Color.White;

        public CBaseCounter(int capacity, int amount)
            : base()
        {
            _capacity = capacity;
            _amount = amount;
            _state = ACTOR_STATES.IDLE;
        }

        public void increment(int amount, bool instant = false)
        {
            _state = ACTOR_STATES.INCREMENT;
            _incrementAmount = amount;
            _instantaneousUpdate = instant;
        
        }

        public void decrement(int amount, bool instant = false)
        {
            _state = ACTOR_STATES.DECREMENT;
            _incrementAmount = -amount;
            _instantaneousUpdate = instant;
        }

        public override void update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.update(gameTime);
            if (_state == ACTOR_STATES.INCREMENT)
            {
                if (_instantaneousUpdate)
                {
                    int allowedIncrement = _capacity - _amount;
                    if (allowedIncrement < _incrementAmount)
                        _incrementAmount = allowedIncrement;

                    _amount += _incrementAmount;
                }
                else if(_amount < _capacity && _incrementAmount > 0)
                {
                    _amount += 1;
                    _incrementAmount -= 1;
                }
            }
            else if (_state == ACTOR_STATES.DECREMENT)
            {
                if (_instantaneousUpdate)
                {
                    int allowedIncrement = _incrementAmount > _amount ? _incrementAmount : _amount;

                    _amount -= allowedIncrement;
                }
                else if(_amount > 0 && _incrementAmount >= _amount)
                {
                    _amount -= 1;
                    _incrementAmount -= 1;
                }
            }
        }

        public override void draw(object sender)
        {
            base.draw(sender);
            Graphics.CGraphics.spriteBatch.DrawString(_sherwood, _amount.ToString(), _position + _textOffset, _textColor);
        }
    }
}

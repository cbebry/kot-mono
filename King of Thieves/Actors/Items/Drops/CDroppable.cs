﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace King_of_Thieves.Actors.Items.Drops
{
    public abstract class CDroppable : CActor
    {
        protected int _capacity; //how much this item provides ex: 1 heart, 10 bombs, 5 arrows

        public CDroppable()
            : base()
        {

        }

        public override void collide(object sender, CActor collider)
        {
            base.collide(sender, collider);
            _killMe = true;
        }

        protected virtual void _yieldToPlayer()
        {
            _killMe = true;
        }

        protected override void _registerUserEvents()
        {
            base._registerUserEvents();

            _userEvents.Add(0, userEventYield);
        }

        protected void userEventYield(object sender)
        {
            _yieldToPlayer();
        }
    }
}

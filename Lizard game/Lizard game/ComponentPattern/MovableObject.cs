﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lizard_game.ComponentPattern
{
    public class MovableObject : Wall
    {
        public MovableObject(GameObject gameObject) : base(gameObject)
        {
        }

        public override void Awake()
        {
            base.Awake();
        }
        public override void Start()
        {
            base.Start();
        }
        public override void Update()
        {
            base.Update();
        }
    }
}

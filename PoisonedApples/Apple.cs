﻿using System.Collections.Generic;

namespace PoisonedApples
{
    public class Apple
    {
        public string Colour { get; set; }
        public bool Poisoned { get; set; }

        public override string ToString()
        {
            return $"{Colour} apple{(Poisoned ? " (poisoned!)" : "")}";
        }
    }
}
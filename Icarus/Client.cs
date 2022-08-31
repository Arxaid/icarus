// This file is part of the Icarus project.
//
// Copyright (c) 2022 Vladislav Sosedov.

namespace Icarus.Core
{
    public class Client
    {
        static void Main(string[] args)
        {
            var IcarusBot = new IcarusCore();
            IcarusBot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
﻿using AutoMapper;
using CodexBank.Common.AutoMapping.Profiles;

namespace CodexBank.Services.Tests.Setup
{
    public class TestSetup
    {
        private static readonly object Sync = new object();
        private static bool mapperInitialized;

        public static void InitializeMapper()
        {
            lock (Sync)
            {
                if (!mapperInitialized)
                {
                    Mapper.Initialize(config =>
                    {
                        config.AddProfile<DefaultProfile>();
                    });

                    mapperInitialized = true;
                }
            }
        }
    }
}

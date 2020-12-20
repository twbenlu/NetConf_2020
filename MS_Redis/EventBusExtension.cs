﻿using EventBus.Base.Standard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MS_Events;
using System.Collections.Generic;

namespace MS_Redis
{
    public static class EventBusExtension
    {
        //列舉的 EventHandler
        public static IEnumerable<IIntegrationEventHandler> GetHandlers()
        {
            return new List<IIntegrationEventHandler>{
                new GPSTriggerEventHandler()
                //若後面擴充了Event，可以在這個地方新增
            };
        }

       //
        public static IApplicationBuilder SubscribeToEvents(this IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<GPSTriggerEvent, GPSTriggerEventHandler>();
            return app;
        }


        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using AutoMapper;
using Ninject;

namespace ParserWebApp.App_Start
{
    public class DependencyInjector : NinjectModule
    {
        public override void Load()
        {
            Bind<Repositories.Interfaces.IUnitOfWork>().To<Repositories.UnitOfWork.UnitOfWork>();
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            
            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                // Add all profiles in current assembly
                cfg.AddProfiles(GetType().Assembly);
            });

            return config;
        }
    }
}
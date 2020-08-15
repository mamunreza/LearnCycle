using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using CustomerManagement.Consumers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInjector;

namespace CustomerManagement
{
    public class Startup
    {
        private readonly Container _container = new Container();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            #region Mass transit hosted service
            //services.AddMassTransit(x =>
            //{
            //    #region Basic
            //    //x.AddConsumer<CustomerAddedConsumer>();
            //    //x.SetKebabCaseEndpointNameFormatter();
            //    //x.UsingRabbitMq((context, cfg) =>
            //    //{
            //    //    cfg.ConfigureEndpoints(context);
            //    //}); 
            //    #endregion


            //    #region xtreme config
            //    //x.AddConsumer<CustomerAddedConsumer>()
            //    //    .Endpoint(e =>
            //    //    {
            //    //        // override the default endpoint name
            //    //        e.Name = "order-service-extreme";

            //    //        // specify the endpoint as temporary (may be non-durable, auto-delete, etc.)
            //    //        e.Temporary = false;

            //    //        // specify an optional concurrent message limit for the consumer
            //    //        e.ConcurrentMessageLimit = 8;

            //    //        // only use if needed, a sensible default is provided, and a reasonable
            //    //        // value is automatically calculated based upon ConcurrentMessageLimit if 
            //    //        // the transport supports it.
            //    //        e.PrefetchCount = 16;
            //    //    });

            //    //x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context)); 
            //    #endregion


            //    #region 2 different consumer for same message in 2 different queues
            //    //x.AddConsumer<CustomerAddedConsumer>();
            //    //x.AddConsumer<CustomerRefineConsumer>();

            //    //x.UsingRabbitMq((context, cfg) =>
            //    //{
            //    //    cfg.ReceiveEndpoint("customer-service", e =>
            //    //    {
            //    //        e.ConfigureConsumer<CustomerAddedConsumer>(context);
            //    //    });

            //    //    cfg.ReceiveEndpoint("refine-service", e =>
            //    //    {
            //    //        e.ConfigureConsumer<CustomerRefineConsumer>(context);
            //    //    });
            //    //}); 
            //    #endregion
            //}); 

            //services.AddMassTransitHostedService();
            #endregion


            services.AddMvcCore();
            services.AddSimpleInjector(_container, options =>
            {
                options
                       //.AddHostedService<MessageConsumerService>()
                       //.AddHostedService<FlatFileImportService>()
                       //.AddHostedService<AddressDataConsumerService>()
                       .AddLogging()
                       .AddAspNetCore()
                       .AddControllerActivation()
                       ;
            });

            _container.AddRabbitMq(Configuration);
            _container.AddMessageConsumptionFeature(Configuration);
            _container.AddOrderMessageConsumptionFeature(Configuration);
            //services.Configure<MessageConsumptionFeatureOptions>(Configuration.GetSection("CustomerDataMessageConsumption"));

            //services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSimpleInjector(_container);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            _container.Verify();
        }
    }
}

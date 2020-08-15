using CustomerManagement.Consumers;
using MassTransit;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class MessageConsumptionFeature
    {
        public void Activate(Container container, FeatureConfiguration<MessageConsumptionFeatureOptions> featureConfiguration)
        {
            container.RegisterInitializer<IBusControl>(bus => ConfigureBus(bus, container, featureConfiguration));

            container.Register<CustomerAddedConsumer>();
            //container.Register<ReservationConsumer<ReservationPlaced>>();
            //container.Register<ReservationConsumer<ReservationUsed>>();
            //container.Register<ReservationConsumer<ReservationCancelled>>();
            //container.Register<InvoiceAddedConsumer>();
            //container.Register<InvoiceDueDateUpdatedConsumer>();
            //container.Register<InvoiceWorkFlowUpdatedConsumer>();
            //container.Register<VoucherAddedConsumer>();
            //container.Register<VoucherCheckOffConsumer>();
            //container.Register<CustomerAnonymizedConsumer>();
            //container.Register<ReservationAdditionalDataConsumer>();

            //container.Register<IMessageProcessor<CustomerUpdated>, CustomerMessageProcessor>();
            //container.Register<IMessageProcessor<ReservationPlaced>, ReservationPlacedMessageProcessor>();
            //container.Register<IMessageProcessor<ReservationCancelled>, ReservationCancelledMessageProcessor>();
            //container.Register<IMessageProcessor<ReservationUsed>, ReservationUsedMessageProcessor>();
            //container.Register<IInvoiceService, InvoiceService>();
            //container.Register<IVoucherService, VoucherService>();
            //container.Register<IMessageProcessor<InvoiceWorkFlowUpdated>, InvoiceWorkFlowMessageProcessor>();
            //container.Register<IMessageProcessor<ReservationAdditionalData>, ReservationAdditionalDataMessageProcessor>();
            //container.Register<ICustomerDataRepository, CustomerDataRepository>();
            //container.Register<IReservationRepository, ReservationRepository>();
            //container.Register<IInvoiceRepository, InvoiceRepository>();
            //container.Register<IVoucherRepository, VoucherRepository>();
            //container.Register<IMessageQueueTrace, MessageQueueTrace>();
            //container.Register<IMessageRedelivery, MessageRedelivery>();

            //if (featureConfiguration.FeatureOptions.IsCleanupActive)
            //{
            //    container.Register<CustomerDataCleanupConsumer>();
            //    container.Register<ICustomerDataCleaner, CustomerDataCleaner>();
            //}

            //container.AddMongoDb(featureConfiguration.MongoDbConfiguration);
        }

        private static void ConfigureBus(IBusControl bus, Container container, FeatureConfiguration<MessageConsumptionFeatureOptions> featureConfiguration)
        {
            var queueName = featureConfiguration.FeatureOptions.QueueName;
            bus.ConnectReceiveEndpoint(queueName, configurator =>
            {
                configurator.Consumer<CustomerAddedConsumer>(container);
                //configurator.Consumer<ReservationConsumer<ReservationPlaced>>(container);
                //configurator.Consumer<ReservationConsumer<ReservationUsed>>(container);
                //configurator.Consumer<ReservationConsumer<ReservationCancelled>>(container);
                //configurator.Consumer<InvoiceAddedConsumer>(container);
                //configurator.Consumer<InvoiceDueDateUpdatedConsumer>(container);
                //configurator.Consumer<InvoiceWorkFlowUpdatedConsumer>(container);
                //configurator.Consumer<VoucherAddedConsumer>(container);
                //configurator.Consumer<VoucherCheckOffConsumer>(container);
                //configurator.Consumer<CustomerAnonymizedConsumer>(container);
                //configurator.Consumer<ReservationAdditionalDataConsumer>(container);

                //if (featureConfiguration.FeatureOptions.IsCleanupActive)
                //{
                //    configurator.Consumer<CustomerDataCleanupConsumer>(container);
                //}

                //configurator.UseMessageRetry(r => r.Immediate(5));
            });
        }
    }
}

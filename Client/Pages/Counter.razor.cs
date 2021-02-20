using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learning.Client.Features;

namespace Learning.Client.Pages {
    public partial class Counter : INotificationHandler<CounterState> {
        public Task Handle(CounterState notification, System.Threading.CancellationToken cancellationToken) {
            Console.WriteLine($"Got ping with data '{notification.Count}'");
            //try {
            //    Console.WriteLine("updating");
            //    Data = notification.Data;
            //    StateHasChanged();
            //    Console.WriteLine("updated");
            //} catch (Exception e) {
            //    Console.WriteLine(e);
            //}
            return Task.CompletedTask;
        }
    }
}

using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualKeyboard.Services.Commands;
using Layout = VirtualKeyboard.Services.Commands.Layout;

namespace VirtualKeyboard.Services
{
    public class ProcessMessageService : IProcessMessageService
    {
        public ProcessMessageService()
        {
            
        }
        public void Notify(int received)
        {
            WeakReferenceMessenger.Default.Send(new TKSetSize(Layout.German, 50));
            switch (received)
            {
                default:
                    
                        break;
            }
            
        }
    }

    
}

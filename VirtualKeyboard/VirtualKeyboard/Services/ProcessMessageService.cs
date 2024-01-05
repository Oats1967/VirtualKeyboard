using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
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
        public void Process(byte[] buffer)
        {
            
            switch (buffer[2])
            {
                case 0x11: // TKSetSize
                    NotifyTKSetSize(buffer);
                    break;
                case 0x13: // TKSetShowPoint
                    NotifyTKSetShowPoint(buffer);
                    break;
                case 0x14: // TKSetShow
                    NotifyTKSetShow(buffer);
                    break;
                case 0x15: // TKSetHide
                    NotifyTKSetHide(buffer);
                    break;
                default: 
                    throw new NotImplementedException();
            }
            
        }

        private void NotifyTKSetHide(byte[] buffer)
        {
            WeakReferenceMessenger.Default.Send(new TKSetHide());
        }

        private void NotifyTKSetShow(byte[] buffer)
        {
            var layout = (Layout)buffer[3];
            WeakReferenceMessenger.Default.Send(new TKSetShow(layout));
        }

        private void NotifyTKSetShowPoint(byte[] buffer)
        {
            var layout = (Layout)buffer[3];
            var x = (buffer[5] << 8) | buffer[4];
            var y = (buffer[7] << 8) | buffer[6];
            WeakReferenceMessenger.Default.Send(new TKSetShowPoint(layout, x, y));
        }

        private void NotifyTKSetSize(byte[] buffer)
        {
            var layout = (Layout)buffer[3];
            var percentage = buffer[4];
            WeakReferenceMessenger.Default.Send(new TKSetSize(layout, percentage));
        }
    }

    
}

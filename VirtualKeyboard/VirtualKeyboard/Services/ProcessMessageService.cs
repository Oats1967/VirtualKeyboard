﻿using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Commands;


namespace VirtualKeyboard.Services
{
    public class ProcessMessageService : IProcessMessageService
    {
        
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
            var layout = (Layouts)buffer[3];
            WeakReferenceMessenger.Default.Send(new TKSetShow(layout));
        }

        private void NotifyTKSetShowPoint(byte[] buffer)
        {
            var layout = (Layouts)buffer[3];
            var x = (buffer[5] << 8) | buffer[4];
            var y = (buffer[7] << 8) | buffer[6];
            WeakReferenceMessenger.Default.Send(new TKSetShowPoint(layout, x, y));
        }

        private void NotifyTKSetSize(byte[] buffer)
        {
            var layout = (Layouts)buffer[3];
            var percentage = buffer[4];
            WeakReferenceMessenger.Default.Send(new TKSetSize(layout, percentage));
        }
    }

    
}

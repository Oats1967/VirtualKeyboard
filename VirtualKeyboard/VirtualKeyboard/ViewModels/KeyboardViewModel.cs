﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using VirtualKeyboard.Services;
using VirtualKeyboard.Services.Commands;

namespace VirtualKeyboard.ViewModels
{
    public abstract partial class KeyboardViewModel : BaseViewModel, IRecipient<TKSetHide>
    {
        protected readonly IKeyboardService _keyboardService;
        public KeyboardViewModel(IKeyboardService service)
        {
            WeakReferenceMessenger.Default.Register(this);
            _keyboardService = service;
        }

        [RelayCommand]
        public void LeftPressed()
        {
            _keyboardService.SendKey(0x25);
        }

        [RelayCommand]
        public void RightPressed()
        {
            _keyboardService.SendKey(0x27);
        }

        [RelayCommand]
        public void EnterPressed()
        {
            _keyboardService.SendKey(0x0D);
           
        }
        public abstract void BackspacePressed();
        public abstract void KeyPressed(string key);

        void IRecipient<TKSetHide>.Receive(TKSetHide message)
        {
            Shell.Current.GoToAsync("..");
        }
    }
}

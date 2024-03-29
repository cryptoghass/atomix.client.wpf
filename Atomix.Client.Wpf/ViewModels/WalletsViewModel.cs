﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Atomix.Subsystems;
using Atomix.Client.Wpf.Common;
using Atomix.Client.Wpf.Controls;
using Atomix.Client.Wpf.ViewModels.Abstract;

namespace Atomix.Client.Wpf.ViewModels
{
    public class WalletsViewModel : BaseViewModel
    {
        public IAtomixApp App { get; set; }
        public IDialogViewer DialogViewer { get; set; }
        public IMenuSelector MenuSelector { get; set; }
        public IConversionViewModel ConversionViewModel { get; set; }

        private ObservableCollection<WalletViewModel> _wallets;
        public ObservableCollection<WalletViewModel> Wallets
        {
            get => _wallets;
            set { _wallets = value; OnPropertyChanged(nameof(Wallets)); }
        }

        private WalletViewModel _selected;
        public WalletViewModel Selected
        {
            get => _selected;
            set
            {
                if (_selected != null)
                    _selected.IsSelected = false;

                _selected = value;

                if (_selected != null)
                    _selected.IsSelected = true;

                OnPropertyChanged(nameof(Selected));
            }
        }

        public WalletsViewModel()
        {
 #if DEBUG
            if (Env.IsInDesignerMode())
                DesignerMode();
#endif
        }

        public WalletsViewModel(
            IAtomixApp app,
            IDialogViewer dialogViewer,
            IMenuSelector menuSelector,
            IConversionViewModel conversionViewModel)
        {
            App = app ?? throw new ArgumentNullException(nameof(app));
            DialogViewer = dialogViewer ?? throw new ArgumentNullException(nameof(dialogViewer));
            MenuSelector = menuSelector ?? throw new ArgumentNullException(nameof(menuSelector));
            ConversionViewModel = conversionViewModel ?? throw new ArgumentNullException(nameof(conversionViewModel));

            SubscribeToServices();
        }

        private void SubscribeToServices()
        {
            App.AccountChanged += OnAccountChangedEventHandler;
        }

        private void OnAccountChangedEventHandler(object sender, AccountChangedEventArgs e)
        {
            Wallets = e.NewAccount != null
                ? new ObservableCollection<WalletViewModel>(
                    e.NewAccount.Wallet.Currencies.Select(currency => new WalletViewModel(
                        app: App,
                        dialogViewer: DialogViewer,
                        menuSelector: MenuSelector,
                        conversionViewModel: ConversionViewModel,
                        currency: currency)))
                : new ObservableCollection<WalletViewModel>();

            Selected = Wallets.FirstOrDefault();
        }

        private void DesignerMode()
        {
            Wallets = new ObservableCollection<WalletViewModel>
            {
                new WalletViewModel {CurrencyViewModel = CurrencyViewModelCreator.CreateViewModel(Currencies.Btc, subscribeToUpdates: false)},
                new WalletViewModel {CurrencyViewModel = CurrencyViewModelCreator.CreateViewModel(Currencies.Ltc, subscribeToUpdates: false)}
            };
        }
    }
}


//Wallets.Add(new WalletViewModel
//{
//    Header = "Bitcoin Gold",
//    IconBrush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Atomix.Client.Wpf;component/Resources/Images/bitcoin_gold.png"))),
//    IconMaskBrush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Atomix.Client.Wpf;component/Resources/Images/bitcoin_gold_mask.png"))),
//    UnselectedIconBrush = Brushes.White,
//    IconPath = "pack://application:,,,/Atomix.Client.Wpf;component/Resources/Images/bitcoin_90x90.png"
//});

/*
    <!--<TabItem Header="Bitcoin">
        <helpers:TabItemHelper.Glyph>
            <iconPacks:PackIconSimpleIcons Kind="Bitcoin" Width="48" Height="48" Foreground="Orange" VerticalAlignment="Center" HorizontalAlignment="Center"/>
        </helpers:TabItemHelper.Glyph>
        <TabItem.Content>
            <local:WalletView/>
        </TabItem.Content>
    </TabItem>
    <TabItem Header="Bitcoin Cash">
        <helpers:TabItemHelper.Glyph>
            <iconPacks:PackIconSimpleIcons Kind="Bitcoin" Width="48" Height="48" Foreground="#4bce50" VerticalAlignment="Center" HorizontalAlignment="Center"/>
        </helpers:TabItemHelper.Glyph>
    </TabItem>
    <TabItem Header="BGold">
        <helpers:TabItemHelper.Glyph>
            <Canvas Width="48" Height="48" Background="White">
                <Canvas.OpacityMask>
                    <ImageBrush ImageSource="Resources/Images/bitcoin_gold_mask.png"/>
                </Canvas.OpacityMask>
            </Canvas>
        </helpers:TabItemHelper.Glyph>
    </TabItem>
    <TabItem Header="Ethereum">
        <helpers:TabItemHelper.Glyph>
            <iconPacks:PackIconSimpleIcons Kind="Ethereum" Width="48" Height="48" Foreground="White" VerticalAlignment="Center" HorizontalAlignment="Center"/>
        </helpers:TabItemHelper.Glyph>
    </TabItem>
    <TabItem Header="Litecoin">
        <helpers:TabItemHelper.Glyph>
            <Canvas Width="48" Height="48" Background="#bebebe">
                <Canvas.OpacityMask>
                    <ImageBrush ImageSource="Resources/Images/litecoin_mask.png"/>
                </Canvas.OpacityMask>
            </Canvas>
        </helpers:TabItemHelper.Glyph>
    </TabItem>
    <TabItem Header="Dash">
        <helpers:TabItemHelper.Glyph>
            <Canvas Width="48" Height="48" Background="#2573c2">
                <Canvas.OpacityMask>
                    <ImageBrush ImageSource="Resources/Images/dash_mask.png"/>
                </Canvas.OpacityMask>
            </Canvas>
        </helpers:TabItemHelper.Glyph>
    </TabItem>
    <TabItem Header="Dogecoin">
        <helpers:TabItemHelper.Glyph>
            <iconPacks:PackIconMaterial Kind="Coin" Width="48" Height="48" Foreground="White" VerticalAlignment="Center" HorizontalAlignment="Center"/>
        </helpers:TabItemHelper.Glyph>
    </TabItem>
    <TabItem Header="Feathercoin">
        <helpers:TabItemHelper.Glyph>
            <iconPacks:PackIconMaterial Kind="Coin" Width="48" Height="48" Foreground="White" VerticalAlignment="Center" HorizontalAlignment="Center"/>
        </helpers:TabItemHelper.Glyph>
    </TabItem>-->

 */
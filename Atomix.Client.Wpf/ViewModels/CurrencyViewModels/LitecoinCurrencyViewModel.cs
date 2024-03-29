﻿using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Atomix.Client.Wpf.Properties;

namespace Atomix.Client.Wpf.ViewModels.CurrencyViewModels
{
    public class LitecoinCurrencyViewModel : BitcoinBasedCurrencyViewModel
    {
        public LitecoinCurrencyViewModel()
        {
            Currency = Currencies.Ltc;
            Header = Currency.Description;
            IconBrush = new ImageBrush(new BitmapImage(new Uri(PathToImage("litecoin.png"))));
            IconMaskBrush = new ImageBrush(new BitmapImage(new Uri(PathToImage("litecoin_mask.png"))));
            AccentColor = Color.FromRgb(r: 191, g: 191, b: 191);
            AmountColor = Color.FromRgb(r: 231, g: 231, b: 231);
            UnselectedIconBrush = Brushes.White;
            IconPath = PathToImage("litecoin.png");
            LargeIconPath = PathToImage("litecoin_90x90.png");
            FeeName = Resources.SvMiningFee;
        }
    }
}
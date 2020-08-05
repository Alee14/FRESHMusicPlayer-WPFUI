﻿using ATL;
using FRESHMusicPlayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FRESHMusicPlayer_WPF_UI_Test.Pages
{
    /// <summary>
    /// Interaction logic for TrackInfoPage.xaml
    /// </summary>
    public partial class TrackInfoPage : Page
    {
        public TrackInfoPage()
        {
            InitializeComponent();
            MainWindow.Player.SongChanged += Player_SongChanged;
            PopulateFields();
        }
        public void PopulateFields()
        {
            Track track = new Track(MainWindow.Player.FilePath);
            if (track.EmbeddedPictures.Count == 0)
            {
                CoverArtBox.Source = null;
                CoverArtRow.Height = new GridLength(0);
            }
            else CoverArtBox.Source = BitmapFrame.Create(new System.IO.MemoryStream(track.EmbeddedPictures[0].PictureData), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            AlbumLabel.Text = $"Album - {track.Album}";
            GenreLabel.Text = $"Genre - {track.Genre}";
            YearLabel.Text = $"Year - {track.Year}";

            TrackNumberLabel.Text = $"Track {track.TrackNumber}/{track.TrackTotal}";
            DiscNumberLabel.Text = $"Disc {track.DiscNumber}/{track.DiscTotal}";

            BitrateLabel.Text = $"Bitrate - {track.Bitrate}kbps";
        }
        private void Player_SongChanged(object sender, EventArgs e) => PopulateFields();

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.Player.SongChanged -= Player_SongChanged;
        }
    }
}

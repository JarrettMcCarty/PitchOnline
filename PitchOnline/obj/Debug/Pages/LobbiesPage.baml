   M S B A M L   `   `   `  ����F  BPitchOnline, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PitchOnlinePitchOnline  5      K GPitchOnline.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:PitchOnline.Core;assembly=PitchOnline.CorePitchOnline.Core 5          PitchOnline.LobbiesPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:PitchOnline     5      Icore8clr-namespace:PitchOnline.Core;assembly=PitchOnline.Core     5      -   5           PitchOnline.BasePage`1   Name$
  Page��5
       N� Title$ RoomPage��5      .��5      �    UIDarkThemeGreyBrush#��[  6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��   IconGrowButton#��[ 6   $��40��5        FontAwesomeArrowLeftIcon#��[ 5        FontAwesome#��[ 5        UIDarkThemeLightGreyBrush#��[ 5      $
��Left=�5      $	��Top=�5       K� ZIndex$ 4��5      ��5      �� *CloseCommand+�� 5!   
   $��50 2 0 0q�6    J� Opacity$	 .75Q�6&   $	��Top=�64   $��Lobbies��6L     FontSizeLarge#��[ 6[     LatoBold#��[ 6�   #��[ 6�   �� 5#   
   $
��Auto=�6   .��5$      �� $��	50 50 0 0q�6   $��Center=�6/   ��6J   �� *Rooms+��5%      �� 5&      .R�5'      $   PitchOnline.LobbiesItemControl  $��0 0 0 5q�63   59      5N      5O      �� 5Q   
   $��40q�6   $��1��6   $��Create Lobby��6.     
PlayButton#��[ 6l   ��6E   �� *CreateLobbyCommand+6l   �� 5R   
   $��1��6   $��2��6!   
���  ��#��[ 6d   #��[ 6�   ��65   �� *ErrorMessage+6�   5S      5T      
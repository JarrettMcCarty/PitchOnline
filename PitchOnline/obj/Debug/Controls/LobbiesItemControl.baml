   M S B A M L   `   `   `  ����F  BPitchOnline, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PitchOnlinePitchOnline  5      K GPitchOnline.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:PitchOnline.Core;assembly=PitchOnline.CorePitchOnline.Core 5      $    PitchOnline.LobbiesItemControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:PitchOnline     5      Icore8clr-namespace:PitchOnline.Core;assembly=PitchOnline.Core     5        � 	Resources  5
         ContentStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name  UIDarkThemeMediumGreyBrush �Name � ColumnDefinitions  UIDarkThemeBlueBrush � ToolTip	 �� 	Converter-   'PitchOnline.BooleanToVisiblityConverter
 �� ConverterParameter �� Padding �� RelativeSource �� CornerRadius � Cursor  FontSizeRegular  LatoBold  DarkerButton �� Padding  ForegroundLightBrush  LatoRegular �� Triggers :� RoutedEvent  UIDarkThemeLightGrey	 �� To g� Duration �� 
TargetName �� TargetProperty P� Property P� Value �� 
TargetName��%�  &	        7  7  7  7  7  7  7  7  7  7  7  �� 5   
   " ��6%   .�5      ��  � �� 5      �� 5      " ��6&   .R�5      �� $ 
background��6!   8��  ._�5      � $ 	container��6#   
������  5   "   �� 5   &   $
��Auto �67   �� 5   &   $��* �67   .+�5   "   �� 8�� $��4��5   %   $ Lobby is open��5   )   $
��Left=�5   %   y�5   %   �� *
IsJoinable+	   $
 1���� 5#   "   $��0��6)   $
 8 15q�5$   %   ._�5(   &   �� $	��115��5)   )   ��5(   -   �� ��5*   )   �� *ActualHeight+ �� *Self+ 5+   )   �� *ActualHeight+ �� *Self+._�5,   *   �� $
 Hand��61   $��20 20 20 20q�6?   $��10 10 10 10q�6T   8�� 8�� 8�� $
��Join��6�   y�6�   �� *
IsJoinable+��6  �� *JoinLobbyCommand+6;  5-   '   5/   #   �� 52   "   $��1��6)   $ 0 0 15 0q�69   ._�53   &   �� $��Center=�61   .+�55   *   �� $ 0 0 0 2q�56   1   8�� $��CharacterEllipsis=�58   1   8�� ��55   4   �� *	LobbyName+59   1   �� 5<   *   $ 0 2 0 0q�5=   0   8�� $��CharacterEllipsis=�5?   0   8�� ��5<   4   �� *LobbyStatus+5@   0   5C   '   5D   #   5F      5G       5J      :� 5L      $ 
MouseEnter��6+   .[�5M   "   �� .��5N   &   �� .��5O   *   �� 8 	 $ 0:0:0.3D�6d   $ 	container��6w   $ Background.Color�6�   5P   '   5Q   #   5R      :� 5S      $ 
MouseLeave��6+   .[�5T   "   �� .��5U   &   �� .��5V   *   �� $ TransparentX�69   $ 0:0:0.3D�6J   $ 	container��6]   $ Background.Color�6   5W   '   5X   #   5Y      P� 5Z       � {� .  . �5[   "   �� $ 	container��6?   8 
  � ��6V   5\      5_      5a      5b      5c      .��5e      �� #��[  5f      5g      
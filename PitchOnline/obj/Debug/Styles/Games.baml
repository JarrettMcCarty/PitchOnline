   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      ? ;Apex, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null3%clr-namespace:Apex.MVVM;assembly=Apex	Apex.MVVM 5      ;)clr-namespace:Apex.Commands;assembly=ApexApex.Commands 5      A,clr-namespace:Apex.DragAndDrop;assembly=ApexApex.DragAndDrop 5      ?+clr-namespace:Apex.Converters;assembly=ApexApex.Converters 5      F BPitchOnline, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PitchOnlinePitchOnline 5      K GPitchOnline.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM8clr-namespace:PitchOnline.Core;assembly=PitchOnline.CorePitchOnline.Core 5	      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089$   PitchOnline.CardBrushConverter   CardBrushConverter  �� DataType"  PitchOnline.Core.PlayingCard � Cursor �� CornerRadius"  Apex.DragAndDrop.DragAndDrop   IsDraggable$  Apex.Commands.ExtendedCommands   RightClickCommand �� RelativeSource �� AncestorType �� Path!   RightClickCommandParameter!  Apex.Commands.EventBindings	   EventBindings*  $Apex.Commands.EventBindingCollection   Apex.Commands.EventBinding
   	EventName   Command   CommandParameter `� 	Converter �� 
TargetType �� Triggers �� Binding �� Value �� Property �� Value"  PitchOnline.CardStackControl �� 
TargetType   IsDragSource   IsDropTarget   PitchOnline.CardStackPanel   FacedownOffset   FaceupOffset   
OffsetMode   NValue �� Orientation  HomeTextStyle  StackMarker  	RunMarker �� 
StartPoint �� EndPoint � Color � Offset  HomeButtonStyle  �� Padding  BetaButtonStyle! � ToolTip" J� Opacity  ComingSoonButtonStyle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      .sys&clr-namespace:System;assembly=mscorlib  5      2apexMVVM%clr-namespace:Apex.MVVM;assembly=Apex  5      :apexCommands)clr-namespace:Apex.Commands;assembly=Apex  5      @apexDragAndDrop,clr-namespace:Apex.DragAndDrop;assembly=Apex  5      >apexConverters+clr-namespace:Apex.Converters;assembly=Apex  5      #gamesclr-namespace:PitchOnline  5      Acore8clr-namespace:PitchOnline.Core;assembly=PitchOnline.Core  5	      %�  &	        (��      5      *, +)7   '  @    &	 M    &	 �    &	 �    &	 S    &	 �	    &	 !       5      6   �� 5      "   6   .R�5   
   �� $	��140��6   $	��190��6   $
 Hand��6*   $��1q�68   $ 6��6L    .  5      ��  �� *FindAncestor+" >�$% ViewModel.RightClickCardCommand� 5      �� 	 5          5      $
 MouseLeftButtonUp��60    6N   ��  �� *FindAncestor+" >�$$ ViewModel.LeftClickCardCommand� 5   1   �� 5      ��5      `� 5      8   .1�5      �� $ CardType�6   �� 5      $ 
IsFaceDown�6   5      ��5       �� 5!      " ��6    5"      �� 5#       6&   �� *
IsFaceDown+ True6E   .a�5$      ��  � �� �	#00ffffff6<   5%      �� 5&       6&   �� *
IsFaceDown+	 False6E   .a�5'      ��  � �� �	#ff6666666<   5(      5*      5,      5-      �� 50      "  6   .�51   
   ��  � �� 52      �� 53      "  6"   .R�54      �� #��z��6   #��z��6G   #��z��6s   ._�55      �� #��z��6'    .  . 
������ ��56      �� 57   "   .R�58   &      6;   �� *FaceDownOffset+ �� "   59   7   �� *FaceUpOffset+ �� "   5:   7   �� *
OffsetMode+ �� "   5;   7   �� *NValue+ �� "   5<   7   �� *Orientation+ �� "  5=   #   5?      5@      5A      5C      5D      �� 5K      " ��6"   .�5L   
   ��  � �� �	#99FFFFFF6'   �� 5M   
    � ��$ 16(�6%   5N      �� 5Q      " ��6    .�5R   
   ��  �@��Padding$ 10q�6$   �� 5S   
    � ��$ 6q�6,   �� 5T   
    �@��CornerRadius$ 15��6)   �� 5U   
    � �� �	#33FFFFFF6(   �� 5V   
    � ��$ 
8,10,40,60q�6#   5W      �� 5Y      " ��6   .�5Z   
   ��  �@��Padding$ 10q�6$   �� 5[   
    � ��$ 6q�6,   �� 5\   
    �@��CornerRadius$ 15��6)   �� 5]   
    � �� 5^      �� 5_      $	 0,0&�6&   $	 0,1&�67   .��5`      � $ 	#33FFFFFF��6#   $ 0Q�65   � 5a      $ 	#00FFFFFF��6#   $	 0.8Q�65   5b      5d      �� 5e   
    � ��$ 
8,10,40,40q�6#   5f      �� 5h      " ��6$   .�5i   
   ��  � �� 5j      �� 5k      " ��6"   .R�5l      �� $  8q�6   $��4q�6)   $ 30��6=   ���	#66FFFFFF���	#11FFFFFF$
 Hand��6~   ._�5m      �� ���	#99FFFFFF$��20(�6L   $��SemiBold"�6d   $��Center=�6�   #��z��6�   5n      5o      5q      5r      �� 5t      " ��6$   .�5u   
   ��  � �� 5v      �� 5w      " ��6"   .R�5x      �� $  8q�6   $��4q�6)   $ 30��6=   ���	#66FFFFFF���	#11FFFFFF$
 Hand��6~   $,! &Currently In Beta ... Try it out now!!��6�   ._�5y      �� $" .9Q�6+   ���	#99FFFFFF$��20(�6Y   $��SemiBold"�6q   $��Center=�6�   #��z��6�   5z      5{      5}      5~      �� 5�      " ��6*   .�5�   
   ��  � �� 5�      �� 5�      " ��6"   .R�5�      �� $  8q�6   $��4q�6)   $ 30��6=   ���	#66FFFFFF���	#11FFFFFF$
 Hand��6~   $! Coming Soon ...��6�   ._�5�      �� $" .5Q�6+   ���	#99FFFFFF$��20(�6Y   $��SemiBold"�6q   $��Center=�6�   #��z��6�   5�      5�      5�      5�      5�      
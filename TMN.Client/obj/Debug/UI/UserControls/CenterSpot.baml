   M S B A M L   `   `   `  ����B  >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls  5      I ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets 5      clr-namespace:TMNTMN  5      !    TMN.UserControls.CenterSpot   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -ucclr-namespace:TMN.UserControls     5      :a,clr-namespace:TMN.Assets;assembly=TMN.Common     5      $accessclr-namespace:TMN     5        � ToolTip  5	      �� *Center.IPAddress+ � 	Resources 5      3  -System.Windows.Media.Effects.DropShadowEffect   shadowEffect   ShadowDepth   Opacity��%:   &	          5   
   $ 0Q�5      $ 1Q�5      5      .��5      �� -   5         ��Name$ containerCanvas��5      $��Center=�5      $��Center=�5       �� Orientation$ Vertical=�5      .+�5   
   �� $��Center=�6   $��Center=�5      $��70��5      .+�5      �� -   5         ��Name$ containerBorder��5       �� Bottom$ 0��5       �� Right$ 0��5      $��70��5      	 �� CornerRadius$	 5��5      $��1q�5      
 � Cursor$

 Hand��5      
���   �
���   �._�5       �� 
�������$��1q�5!      $	 4��5"      ._�5#      � -   5         �Name$ containerGrid��5#       � RowDefinitions 5$      �� 5%      $��25 �6,   �� 5&      $��25 �6-   �� 5'      $��0 �6-    5)       �� 
TargetType �� Property �� Value��%c  '��       '�� �     �� 5*      " ��6$   .�5+   "   ��  � ��
 �����5,   %   �� 5-   "    � ��$ Center=�5.   %   �� 5/   "    � ��$ Center=�50   %   �� 51   "    � ��$ 0q�52   %   53      �� 54      " ��6$   .�55   "   ��  � ��$
 0.01q�56   %   �� 57   "    �@��CornerRadius$ 3��58   %   59      5:      .+�5;      �� -   5        $ switchBorder��5<   !   $	 3,3,0,0��5;   !   ��5=      
� 5>   "    
� RadiusX$ 1Q�66    
� RadiusY$ .6Q�5?   2    
� Center$ .5,.5&�5@   2    
� GradientOrigin$
 .5,0&�5A   2   .��5B   &   � -   5        SwitchColor�� � Offset$ 0Q�5C   /   5D   #   5F      �� 5G      -   5        $ sensorBorder��5H   %   $��1��5G   !   $	 0,0,0,0��5I      ��5J      
� 5K   "   $ 1Q�66   $ .6Q�5L   2   $ .5,.5&�5M   2   $
 .5,1&�5N   2   .��5O   &   � -   5        SensorColor��$ 0Q�5P   /   5Q   #   5S      �� 5T      -   5         ��Name$ switchNameLabel��5W      $��0��5T       $��16(�5V       ��5U       �� *Center.SwitchType.Name+5W      �� 5X      -	   5        $ sensorLabel��5Y       $��1��5X       $��Normal"�5Z        J� Opacity$ .8Q�5[       $��12(�5\       $��Italic$�5]       .��63   Sensor6;   �� 5^      $��4��6!   $ .6Q�5_   !     GlassFilter#��[ 5`   !   �� 5a      -
   5        $ switchHyper��5a   B    K� ZIndex$	 100��6X   $��70��6k   $
��auto��6v   $

 Hand��6�   
������ 6�   �� 5b      -   5        $ sensorHyper��5b   B   $	 100��6W   $��1��6j   $��70��6w   $
��auto��6�   $

 Hand��6�   
������ 6�   5c      5d      5e      "   TMN.UserControls.SoundButton  5f      -   5          Name$ muteAllButton��5f      {�.  $y�Hidden=�6G      IsMute . $	 -12��6j   $	 -15��6~   !   TMN.UserControls.MuteCenter  5g      -   5          Name$ 
muteButton��5g      {�.  $y�Hidden=�6C      IsMute . $	 -10��6f   $	 -15��6z   5i      �� 5j   
   -   5        $��lblCenterName��5k      $��40��5j      $��	B Nazanin)�5l      $
��Bold"�5m      $��25(�5n       �� TextAlignment$ Center=�5p      $��Bottom=�5q      $��Center=�5r      ��5o      �� *Center.Name+��5s      �� 5t       �� EndPoint$	 0,1&�6&   .��5u      � $ 0Q�6#    � Color$ Black��5v   #   � 5w      $ .5Q�6"   $ Black��5x   "   � 5y      $	 1.5Q�6%   $
 gray��5z   %   5{       J� Effect 5}        5~      $ 1Q�6#       	Direction$	  -90Q�5   #   $ .1Q�5�   #   !   
BlurRadius$! 1Q�5�   #   "   Color$" White��5�   #   5�   "   5�      # � ContextMenu# 5�      �� 5�   
   -   5        .@�5�      m� -   5        $ m�Name$$ connectMenuItem��5�      $��اتصال به مرکز��5�      5�      m� 5�      -   5        $$ 
connectVPN��5�      $��اتصال به VPN��5�      % m� IsCheckable% . & m� 	IsChecked& . 5�      m� 5�      -   5        $$ muteMenuItem��5�      $��قطع صدا��5�      % . & .  5�   E   m� 5�      -   5        $$ muteCriticalMenuItem��5�      $��قطع صدای Critical��5�      % . & .  5�   M   m� 5�      -   5        $$ muteMajorMenuItem��5�      $��قطع صدای Major��5�      % . & .  5�   J   m� 5�      -   5        $$ muteMinorMenuItem��5�      $��قطع صدای Minor��5�      % . & .  5�   J   m� 5�      -   5        $$ muteSensorMenuItem��5�      $ ��قطع صدای سنسور��5�      % . & .  5�   .   5�      5�      
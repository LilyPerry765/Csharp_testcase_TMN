   M S B A M L   `   `   `  ����B  >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null)clr-namespace:TMN.DiagramTMN.Diagram  5   	   I ETMN.Common, Version=2.0.1.40989, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   -%clr-namespace:TMN;assembly=TMN.CommonTMN 5   	   M ITMN.Converters, Version=1.1.0.26823, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters 5   	   U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf 5	   	   %    TMN.UI.Windows.CenterLinkWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    5   	   )dgmclr-namespace:TMN.Diagram 	    5   	   :a,clr-namespace:TMN.Assets;assembly=TMN.Common 	    5   	   6sys&clr-namespace:System;assembly=mscorlib 	    5   	   7enums%clr-namespace:TMN;assembly=TMN.Common 	    5   	   Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters 	    5   	   Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf 	    5	   	     � Title$$  ارتباط بين مراكز��5
   	   $��RightToLeft=�5   	   .��5      ]�  � 	Resources 5   
    j� Stretch j� 
AlignmentX j� 
AlignmentY   map �� Opacity �� ImageSource%  TMN.Assets.ImageSourceExtension   Width��%�   &	        �� 5      $
 None=�6   $
 Left=�5      $	 Top=�5      $ .3Q�5       5        *iranMap.jpg+$	 500Q�5      5      .+�5   
   � $
��Left=�6   $	��120��5       � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    	 J� BitmapEffect	 5      �� 5      
 �� Relief$
 .1Q�6$    �� 
BevelWidth$ 1Q�5   $    �� 
LightAngle$ 45Q�5    $    �� EdgeProfile$ 	CurvedOut=�5!   $    �� 
Smoothness$ 1Q�5"   $   .+�5$      9�  9� 
IsExpanded . $��0��5%      ���	#33FFFFFF��5'      �� 5(      $��ابزارها��6   .��5*      �  5+      �� 5,      $
��Auto �6(   �� 5-      $
��Auto �6(   .+�5/      �� -   5         ��Name$ 
btnRefresh��5/      $��30��50      $��5q�51      $��0��52       � ToolTip$ به روزرساني��53      .��55      ��  �� Orientation$ 
Horizontal=�6%   .+�56      �� $��16��6$   $	��2,0q�57   $   ��58   $        Path$ refresh.png���� 59      $��به روزرساني��6$   5:      5;      � 5<      $��1��6   $��5q�5=       � ColumnDefinitions 5>      �� 5?      $��22 �60   �� 5@      $��* �60   �� 5A      $��22 �60   .+�5C      �� -   5        $ 
btnZoomOut��5C   !   $��0q�5E   !   .��5F      �� $��13��6$   ��5G   $     $ zoomout.png��5H      �� 5I      -   5         ��Name$ 
ZoomSlider��5I   !    � Minimum$ .1Q�5J   !    � Maximum$ 1Q�5K   !    � SmallChange$ .1Q�5L   !    � LargeChange$ .5Q�5M   !    � Value$ 1Q�5N   !   $��0q�5O   !   $��1��5P   !   �� 5Q      -   5        $ 	btnZoomIn��5Q   !   $��2��5R   !   $��0q�5T   !   .��5U      �� $��13��6$   ��5V   $     $ 
zoomin.png��5W      5X      5Y      5Z      9� 5[       . $��1��5\      ���	#33FFFFFF��5^      �� 5_      $��ارتباط��6   .��5a      � $
��Auto��6    5c      �� 5d      $
��Auto �6(   $��74��5e   (   �� 5f      $
��Auto �6(   �� 5g      $
��Auto �6(   �� 5h      $
��Auto �6(   �� 5i      $
��Auto �6(   .+�5k      �� -   5         ��Name$ label1��5m       $��1��5k       $��5,5,0,0q�5l       $	��Top=�5n       $
��Left=�5o       $
��Auto��5p       نوع لينک:68   �� 5q      -   5         ��Name$ LinkTypesListBox��5s      $��2��5q      $	��Top=�5r      $��0,5,5,0q�5t      �� 5u      -   5        $ label2��5w       $��3��5u       $��5q�5v       $
��Left=�5x       ظرفيت:6C   "  Enterprise.Wpf.NumericUpDown  5�      -   5          Name$ E1CountNumericUpdown��5�   &      Minimum$ 1X�5�   &   $��4��5�   &   $��21��5�   &   $��5,0,0,10q�5�   &   $
��Left=�5�   &   $��Center=�5�   &   d� 5�      -	   5         d�Name$ btnLine��5�   #   $��5q�5�   #   $��30��5�   #   $ ايجاد ارتباط��5�   #   $��0��5�   #     d� 	IsChecked$  FalseY�5�   #   .��5�      �� $ 
Horizontal=�6%   .+�5�      �� $	��2,0q�6%   $��24��5�   %   ��5�   %     $ Link.png���� 5�      .��6$   ايجاد ارتباط62   5�      5�      5�      5�      � 5�      $��2��6    5�      �� 5�      �� 5�      .+�5�      �� $	��5,1q�6   $
��Left=�5�      $��0��5�      فيلتر:61   �� 5�      -
   5        ! ��Name$! cmbCenterType��5�      $��1��5�      $	��5,1q�5�      $��20��5�      " �� SelectedValuePath$
" Enum��5�      $��Stretch=�5�      ��5�        TMN.Assets.EnumDataSource  #   EnumType$# enums:CenterTypesL�5�      R� 5�      -   5        $ R�Name$
$ Tree��5�      $��3��5�      
�������
������ $��0q�5�      $��2q�5�      ��5�      �� 5�      .�5�      -   5        �� 5�      % �� Property% � ��& �� Value
& �   �5�   !   �� 5�      % �@J�Effect& 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�   "   '   ShadowDepth$' 1Q�63   5�      �� 5�      % � ��$& 2q�5�   !   5�      ��5�      �� 5�      .R�5�         TMN.Diagram.CenterItem  $��50��6*   $��20��5�   *    5�   *   �� *
CenterType+)  #TMN.Converters.CenterTypesConverter(   Instance) �� 	Converter#) Z( ��5�      �� 5�   "   * �� 
StartPoint$	* 1,0&�66   + �� EndPoint$	+ 0,1&�5�   6   .��5�   &   � , � Color$, White��63   - � Offset$- 0Q�5�   3   � 5�   &   $- 2Q�5�   3   , 5�   3   �� *
CenterType+.  (TMN.Converters.CenterTypesColorConverter.   Instance#) Z. 5�   3   5�   #   5�      5�      5�      5�      �� 5�   
   -   5        / ��Name$/ scrollViewer��5�      $
��Auto=�5�      $
��Auto=�5�      .��5�      �� -   5        0 ��Name$
0 Root��5�      $	��Top=�5�      $
��Left=�5�      $
��7000��5�      $
��4000��5�      #��[  5�      1 � LayoutTransform1 5�      �� 5�      2 �� ScaleX2 6%   �� *Value+3 �� ElementName$3 
ZoomSlider��4 �� ScaleY4 5�   %   �� *Value+$3 
ZoomSlider��5�      5�      5�      5�      
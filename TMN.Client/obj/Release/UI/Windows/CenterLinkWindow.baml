   M S B A M L   `   `   `  ���� B  >TMN, Version=2.2.0.19582, Culture=neutral, PublicKeyToken=null)clr-namespace:TMN.DiagramTMN.Diagram  I ETMN.Common, Version=2.0.1.19574, Culture=neutral, PublicKeyToken=null;,clr-namespace:TMN.Assets;assembly=TMN.Common
TMN.Assets O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem -%clr-namespace:TMN;assembly=TMN.CommonTMN M ITMN.Converters, Version=1.1.0.19574, Culture=neutral, PublicKeyToken=nullG4clr-namespace:TMN.Converters;assembly=TMN.ConvertersTMN.Converters U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf %    TMN.UI.Windows.CenterLinkWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R	 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 	    )dgmclr-namespace:TMN.Diagram 	    :a,clr-namespace:TMN.Assets;assembly=TMN.Common 	    6sys&clr-namespace:System;assembly=mscorlib 	    7enums%clr-namespace:TMN;assembly=TMN.Common 	    Econv4clr-namespace:TMN.Converters;assembly=TMN.Converters 	    Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf 	      � Title$$  ارتباط بين مراكز��$��RightToLeft=�.��]�  � 	Resources  j� Stretch j� 
AlignmentX j� 
AlignmentY   map �� Opacity �� ImageSource%  TMN.Assets.ImageSourceExtension   Width��%[   &	        �� $
 None=�$
 Left=�$	 Top=�$ .3Q�   *iranMap.jpg+$	 500Q�.+�� $
��Left=�$	��120�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �	 J� BitmapEffect	 �� 
 �� Relief$
 .1Q� �� 
BevelWidth$ 1Q� �� 
LightAngle$ 45Q� �� EdgeProfile$ 	CurvedOut=� �� 
Smoothness$ 1Q�.+�9�  9� 
IsExpanded . $��0�����	#33FFFFFF���� $��ابزارها��.���  �� $
��Auto ��� $
��Auto �.+��� -    ��Name$ 
btnRefresh��$��30��$��5q�$��0�� � ToolTip$ به روزرساني��.����  �� Orientation$ 
Horizontal=�.+��� $��16��$	��2,0q���     Path$ refresh.png���� $��به روزرساني��� $��1��$��5q� � ColumnDefinitions �� $��22 ��� $��* ��� $��22 �.+��� -   $ 
btnZoomOut��$��0q�.���� $��13����  $ zoomout.png���� -    ��Name$ 
ZoomSlider�� � Minimum$ .1Q� � Maximum$ 1Q� � SmallChange$ .1Q� � LargeChange$ .5Q� � Value$ 1Q�$��0q�$��1���� -   $ 	btnZoomIn��$��2��$��0q�.���� $��13����  $ 
zoomin.png��9�  . $��1�����	#33FFFFFF���� $��ارتباط��.��� $
��Auto�� �� $
��Auto �$��74���� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� -    ��Name$ label1��$��1��$��5,5,0,0q�$	��Top=�$
��Left=�$
��Auto��نوع لينک:�� -    ��Name$ LinkTypesListBox��$��2��$	��Top=�$��0,5,5,0q��� -   $ label2��$��3��$��5q�$
��Left=�ظرفيت:"  Enterprise.Wpf.NumericUpDown  -     Name$ E1CountNumericUpdown��   Minimum$ 1X�$��4��$��21��$��5,0,0,10q�$
��Left=�$��Center=�d� -	    d�Name$ btnLine��$��5q�$��30��$ ايجاد ارتباط��$��0��  d� 	IsChecked$  FalseY�.���� $ 
Horizontal=�.+��� $	��2,0q�$��24����  $ Link.png���� .��ايجاد ارتباط� $��2�� �� �� .+��� $	��5,1q�$
��Left=�$��0��فيلتر:�� -
   ! ��Name$! cmbCenterType��$��1��$	��5,1q�$��20��" �� SelectedValuePath$
" Enum��$��Stretch=���  TMN.Assets.EnumDataSource  #   EnumType$# enums:CenterTypesL�R� -   $ R�Name$
$ Tree��$��3��
�������
������ $��0q�$��2q����� .�-   �� % �� Property% � ��& �� Value
& �   ��� % �@J�Effect& 3  -System.Windows.Media.Effects.DropShadowEffect  '   ShadowDepth$' 1Q��� % � ��$& 2q����� .R�   TMN.Diagram.CenterItem  $��50��$��20�� �� *
CenterType+)  #TMN.Converters.CenterTypesConverter(   Instance) �� 	Converter#) Z( ���� * �� 
StartPoint$	* 1,0&�+ �� EndPoint$	+ 0,1&�.��� , � Color$, White��- � Offset$- 0Q�� $- 2Q�, �� *
CenterType+.  (TMN.Converters.CenterTypesColorConverter.   Instance#) Z. �� -   / ��Name$/ scrollViewer��$
��Auto=�$
��Auto=�.���� -   0 ��Name$
0 Root��$	��Top=�$
��Left=�$
��7000��$
��4000��#��[  1 � LayoutTransform1 �� 2 �� ScaleX2 �� *Value+3 �� ElementName$3 
ZoomSlider��4 �� ScaleY4 �� *Value+$3 
ZoomSlider��
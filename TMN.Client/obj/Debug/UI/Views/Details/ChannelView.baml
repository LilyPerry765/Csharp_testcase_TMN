   M S B A M L   `   `   `  ����B  >TMN, Version=2.8.0.37957, Culture=neutral, PublicKeyToken=null3clr-namespace:TMN.UserControlsTMN.UserControls  5      U QEnterprise.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=80f1ae88e0a8b4d1G4clr-namespace:Enterprise.Wpf;assembly=Enterprise.WpfEnterprise.Wpf 5      #    TMN.Views.Details.ChannelView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -ucclr-namespace:TMN.UserControls     5      Br4clr-namespace:Enterprise.Wpf;assembly=Enterprise.Wpf     5      .��5      �� -   5          �� Orientation$  
Horizontal=�5      .+�5   
   �� .+�5	      �� $��Right=�6    �� Target 5
      ��  �� ElementName$ RouteComboBox��.��6C   	مسير:6J   �� 5      $��Right=�6    5      �� $ TimeSlotNumericUpDown��.��6K   تايم اسلات:6X   �� 5      $��Right=�6    5      �� $ ChannelNumericUpDown��.��6J   کانال:6R   �� 5      $��Right=�6    5      �� $ 	CICUpDown��.��6?   CIC:6E   5      �� 5   
   .+�5      %  TMN.UserControls.EntityComboBox  -   5          Name$ RouteComboBox��5       $	��140��5       �� 
IsEditable .  �� DisplayMemberPath$
 TGNO��5       �� SelectedValuePath$ ID��5      $��0,2,0,2q�5      $
��Left=�5       �� SelectedValue 5      �� *	RouteID+5      "  Enterprise.Wpf.NumericUpDown  5      -   5          Name$ TimeSlotNumericUpDown��5      $��0,2,0,2q�5      $
��Left=�5      	   Value	 5      �� 
 �� Path$
 TimeSlot� �� Mode$ TwoWay=�5        5      -   5        $ ChannelNumericUpDown��5      $��0,2,0,2q�5!      $
��Left=�5"      	 5       �� $	
 LNO�$ TwoWay=�5"      �� 5#      $  
Horizontal=�6   .+�5$      �� ��6   �� *
TimeSlot+�� 5%      $��Center=�6   -6:     5&      -   5        $ 	CICUpDown��5&   "   $��0,2,0,2q�5(   "   $
��Left=�5)   "   	 5'   "   �� $
 Link.CIC�$ TwoWay=�5)   "   5+      5,      5-      5/      
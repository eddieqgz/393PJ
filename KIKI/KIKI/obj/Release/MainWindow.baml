   M S B A M L   `   `   `  ���� ?  ;KIKI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:KIKIKIKI  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem     KIKI.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:KIKI     \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  3System&clr-namespace:System;assembly=mscorlib    � Title$  Meeting Assistant��$	��540��$	��480�� � 
ResizeMode$ CanMinimize=�
������� � 	Resources    TabItemStyle1 �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Padding	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName �� Color �� Opacity  TabItemStyle2 
 Brush1 �� EndPoint �� 
StartPoint � Color � Offset��%�  &	        &	 �    &	     �� " ��.���  � �� �� " ��.R��� $ Border��$ 74,10,75,10q�
�������._��� $	 ContentSite��$��Header��
 P�  �@��
IsSelected . . ��� $ ContentSite�� � ��
 ������� $ Border�� � �� �� $ White��$	 0.2Q�P�  �@��
IsSelected .  . ��� $ ContentSite�� � ��
 ������� $ Border�� � �� �� $ Black��$	 0.2Q��� " ��.���  � �� �� " ��.R��� $ Border��$ 78,10,77,10q�
�������._��� $	 ContentSite��$��Header��
 P�  �@��
IsSelected . . ��� $ ContentSite�� � ��
 ������� $ Border�� � �� �� $ White��$	 0.2Q�P�  �@��
IsSelected .  . ��� $ ContentSite�� � ��
 ������� $ Border�� � �� �� $ Black��$	 0.2Q��� $ 0.5,1&�$ 0.5,0&�.��� $ 	#FF38387C��$ 0Q�� $ 	#FFFF8305��$ 1Q�.���  J� Opacity$	 0.9Q����	#FF16428C � ColumnDefinitions �� �� $��0* � J� OpacityMask �� $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 0Q�� $ White��$ 1Q�� $ 	#FF484848��� $ White��$	 0.4Q�� $ 	#FF342020�� � RowDefinitions �� $��3* ��� $��7* �.+��� -    ��Name$ 	mlistView��$��Center=�$	��125��$��-3,27,-3.4,0q�$	��Top=�$	��480��$��2��$��20(�
�������$��Center=�$��Center=�$��Hidden=�$��Hidden=���Z� ���� $ 	#FFABADB3��$ 0Q�.@�   ���� .R�� .+��� $��25��$	��130��$��12(�$��Microsoft YaHei UI Light)�$��Center=� �� 
LineHeight$ 6��$
��Auto=�$��Center=� J� RenderTransformOrigin$ 0.5,0.5&� �� Padding$ 10,0,0,0q����� *Time+z�V� .~��� �� �� T�  T� Y$ 5Q��� $��25��$	��190��$��12(�$��Microsoft YaHei UI Light)�$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&� �� TextAlignment$ Center=����� *Name+z�V� .~��� �� �� T� $ 5Q��� $��25��$	��140��$��12(�$��Microsoft YaHei UI Light)�$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&�$ Center=����� *
Attendee+z�V� .~��� �� �� T� $ 5Q��� -    ��Name$ textBox��$��27��$
��Wrap=�$��Today's Events��$	��Top=�$	��480��
�������$��
0,0,-6.4,0q�$
��Left=�$ 	200,5,0,0q����� $ Black��$	 0.3Q��� -   $��loginButton��$��388,0,9.6,125.8q�
���������Z� ��Z� .��	Log Out�� -    ��Name$ 
tabControl��$
��Left=�$	��358��$��-3,152,-0.4,0q�$	��Top=�$	��477��$��2��$��2����Z� ���� $ White��$ 0Q�.@��� $��Previous Meeting��#���   $��Microsoft YaHei)�$	��1,0q���Z� ���� $ White��$	 0.2Q�.��� $&�� 0,0                       ,0.2,0q� �� $	��95* ��� $	��62* �.+��� -   $ 
mlistView4��$
��Left=�$��	0,0,0,1.4q�$	��474��$��0q�$	��280��$��Bottom=�$��Hidden=�$��Hidden=�|�.  $��2����Z� ���� $ 	#FFABADB3��$ 0Q�.@�   ���� .R��� $ 
mlistView5��$��Center=�$��Stretch=�$
��Auto��$	��Top=�$	��460��$��2��$��20(�
�������$��Center=�$��Hidden=�$��Disabled=�|�.  $ 0.5,0.5&���Z� ��Z� z�V� .~��� �� �� T�   T� X$	  -10Q�.@�� $
��auto��$
��auto����. $ 0.5,0.5&�z�V� .~��� �� �� T� .+��� $��27��$
��auto��$ 0.5,0.5&�$��0q�$ 10,5,0,0q�$��14(�$��Microsoft YaHei UI)�$
��Bold"����� *Date+��Z� ���� $ Black��$	 0.8Q�z�V� .~��� �� �� T� $  -1Q��� $��27��$	��110��$ 0.5,0.5&�$��	-10,0,0,0q�$ 10,5,0,0q�$��12(�$��Normal$�$��Microsoft YaHei UI Light)����� *Time+��Z� ���� $ White��$	 0.8Q�z�V� .~��� �� �� T� $  -1Q��� $
��haha��-   $��25��$	��200��
�������$��12(�$��Microsoft YaHei UI Light)�$��0,-5,0,0q�
! � Tag! �� *Docs+���� *Name+��Z� ��Z� " � LayoutTransform" V� .~��� �� �� T� $ 5Q��� $
��Wrap=�$
��auto��$	��150��$��12(�$��Microsoft YaHei UI Light)�$��Stretch=�$ 6��$
��Auto=�$
��Left=�$ 0.5,0.5&�$ Center=�$��0,-2,0,5q����� *
Attendee+z�V� .~��� �� �� T� $ 5Q��� $��Recent Files��#���  ��. ��Z� ���� $ White��$	 0.2Q�.��� $��	0,0,0.2,0q�$��Hidden=���. $��Hidden=� �� $	��53* ��� $
��104* �.+��� -   $ 
RecentFile��$
��Left=�$
��auto��$	��Top=�$	��470��$��2��$��20(�
�������$
��Left=�$��Center=�# J� IsHitTestVisible# . |�. $��Hidden=�$��0,30,0,0q�$��Hidden=�$ 0.5,0.5&���Z� ��Z� z�V� .~��� �� �� T� $  -5Q�.@�   ���� .R�� $
��auto����. |�. $ 0.5,0.5&�z�V� .~��� �� �� T� $  -5Q�.+��� $��27��$
��auto��$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&�$ 10,0,0,0q�z�V� .~��� �� �� T� $ 5Q��� -   ���� *URL+.����� $ Black��$	 0.8Q�.�.��� $��14(�$��Microsoft YaHei UI)�$
��Bold"����� *Name+���� $ 	#FF050505��$	 0.8Q��� $��27��$	��120��$��12(�$��Microsoft YaHei UI Light)�$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&�$ 10,0,0,0q����� *Time+z�V� .~��� �� �� T� $ 5Q��� -	   $��27��$	��200��
�������$��12(�$��Microsoft YaHei UI Light)�! �� *Files+���� *Title+��Z� ��Z� �� $
��Wrap=�$��27��$	��135��$��12(�$��Microsoft YaHei UI Light)�$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&�$ Center=����� *
Attendee+z�V� .~��� �� �� T� $ 5Q�
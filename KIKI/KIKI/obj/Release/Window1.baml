   M S B A M L   `   `   `  ���� ?  ;KIKI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:KIKIKIKI      KIKI.Window1   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:KIKI       � Title$  Search��$
��auto��$	��480�� � 
ResizeMode$ NoResize=� � 	Resources    TabItemStyle1 �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Padding	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName �� Color �� Opacity  TabItemStyle2��%4  &	        &	 �    �� " ��.���  � �� �� " ��.R��� $ Border��$ 105,10,105,10q�
�������._��� $	 ContentSite��$��Header��
 P�  �@��
IsSelected . . ��� $ ContentSite�� � ��
 ������� $ Border�� � �� �� $ White��$	 0.2Q�P�  �@��
IsSelected .  . ��� $ ContentSite�� � ��
 ������� $ Border�� � �� �� $ Black��$	 0.2Q��� " ��.���  � �� �� " ��.R��� $ Border��$ 90,10,95,10q�
�������._��� $	 ContentSite��$��Header��
 P�  �@��
IsSelected . . ��� $ ContentSite�� � ��
 ������� $ Border�� � �� �� $ White��$	 0.2Q�P�  �@��
IsSelected .  . ��� $ ContentSite�� � ��
 ������� $ Border�� � �� �� $ Black��$	 0.2Q�.��� $	��480������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FF87C397�� � Offset$ 0Q�� $ 	#FF128ADF��$ 1Q�.+��� -    ��Name$ textBox��$
��Left=�$��23��$��	10,10,0,0q� �� TextWrapping$
 Wrap=�$��Enter keyword here��$	��Top=�$	��371�� J� RenderTransformOrigin$ 0.164,0.289&���Z� .������ $ Black��$	 0.2Q�.���� -    ��Name$ button��$��Search��$
��Left=�$��
394,10,0,0q�$	��Top=�$��65��$ 1.182,0.182&�$��23��
���   ���Z� ���� $ Black��$	 0.2Q��� -    ��Name$ 
tabControl��$
��Left=�$
��auto��$��0,38,0,0q�$	��Top=�$	��480����Z� ��Z� .@��� $��Files��#���   .��� $	��Top=�.+�� $
��auto��$	��475��$ 0.5,0.5&�|�. z�V� .~��� �� �� T�  T� X$ -7Q�.+��� -    ��Name$ 
RecentFile��$��Center=�$
��auto��$	��Top=�$	��480��$��2��$��20(�
�������$
��Left=�$��Center=� J� IsHitTestVisible . |�. $��Hidden=�$��Hidden=���Z� ��Z� .@�   ���� .R�� $
��auto����. |�. .+��� $��25��$
��auto��$��Center=� �� 
LineHeight$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&� �� Padding$ 10,0,0,0q�z�V� .~��� �� �� T�  T� Y$ 5Q��� -   ���� *URL+.����� $ Black��$	 0.8Q�.�.��� $��12(�$��Microsoft YaHei UI)�$
��Bold"����� *Name+���� $ Black��$ 0.795Q��� $��25��$
��Auto��$��12(�$��Microsoft YaHei UI Light)�$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&�$ 10,0,0,0q����� *Time+z�V� .~��� �� �� T� $ 5Q��� $
��haha��-   $��25��$	��155��
�������$��12(�$��Microsoft YaHei UI Light)�
 � Tag �� *Files+���� *Title+��Z� ��Z� �� $��25��$	��150��$��12(�$��Microsoft YaHei UI Light)�$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&�  �� TextAlignment$  Center=����� *
Attendee+z�V� .~��� �� �� T� $ 5Q��� $��Meetings��#���  .��� .+��� -   $ 
mlistView5��$
��Left=�$
��auto��$	��Top=�$��2��$��20(�
�������$
��Left=�$��Center=�$��Hidden=�$��Disabled=���Z� ��Z� ���� .R�� $
��auto����. $
��Left=�.+��� $��25��$	��120��$��12(�$��Microsoft YaHei UI Light)�$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&�$ 10,0,0,0q����� *Time+z�V� .~��� �� �� T� $ 5Q��� $
��haha��-   $��25��$	��200��
�������$��12(�$��Microsoft YaHei UI Light)� �� *Docs+���� *Name+��Z� ��Z� �� $��25��$	��130��$��12(�$��Microsoft YaHei UI Light)�$��Center=�$ 6��$
��Auto=�$��Center=�$ 0.5,0.5&�$  Center=����� *
Attendee+z�V� .~��� �� �� T� $ 5Q�
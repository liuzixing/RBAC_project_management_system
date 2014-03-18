<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsDateTree.ascx.cs"
    Inherits="Control_NewsDateTree" %>
<div id="newsClass">
    
    
    <ul id="contentClass">
<!-- 
     <li class="title5"> 
        <a class="newsArchive">+新闻归档</a>
         </li> -->


     
        <asp:Repeater ID="dateTree" runat="server" >
          
            <ItemTemplate>
            
                <li class="newslist_classify">
                    <div class="newslist_classify_title">
                        <div class="li_icon">
                        </div>
                        <a class="newslist_classify_label " id="year_<%#((System.Xml.XmlNode)Container.DataItem).Attributes["value"].Value%>" href="NewsTempate.aspx?NewsYear=<%#((System.Xml.XmlNode)Container.DataItem).Attributes["value"].Value%>&month=" target ="_self" >
                           <span class="jia">+</span> <%#((System.Xml.XmlNode)Container.DataItem).Attributes["value"].Value%></a>
                           </div>
                   <br />
                 <br />
                    <asp:Repeater ID="monthList" runat="server"  DataSource='<%#((System.Xml.XmlNode)Container.DataItem).ChildNodes %>'>
                    <ItemTemplate>                  
           
                    <div class="newslist_classify_content" >
                    <a  class="newslist_classify_label1 " id="year_<%#((System.Xml.XmlNode)Container.DataItem).ParentNode.Attributes["value"].Value %>_month_<%#((System.Xml.XmlNode)Container.DataItem).InnerText%>"  href="NewsTempate.aspx?NewsYear=<%#((System.Xml.XmlNode)Container.DataItem).ParentNode.Attributes["value"].Value %>&month=<%#((System.Xml.XmlNode)Container.DataItem).InnerText%>" target ="_self"><%#((System.Xml.XmlNode)Container.DataItem).InnerText%>月</a>
                    </div>
         
                    
                       </ItemTemplate>
                     </asp:Repeater>
                </li>
                
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewRecruitsCategory.ascx.cs" Inherits="Control_NewRecruitsCategory" %>
<div id="newsClass">
    
    
    <ul id="contentClass">   
     
        <asp:Repeater ID="dateTree" runat="server" >
          
            <ItemTemplate>
            
                <li class="newslist_classify">
                    <div class="newslist_classify_title">
                        <div class="li_icon">
                        </div>
                       <a class="newslist_classify_label "href="#" >
                           <!-- <span class="jia">+</span>-->
                           
                            <%#((System.Xml.XmlNode)Container.DataItem).Attributes["value"].Value%></a>
                           </div>
                           
                   <br />
                 <br />
                    <asp:Repeater ID="monthList" runat="server"  DataSource='<%#((System.Xml.XmlNode)Container.DataItem).ChildNodes %>'>
                    <ItemTemplate>                  
           
                    <div class="newslist_classify_content">
                    <a class="NewRecruitsCategory" href="NewRecruitsList.aspx?NewRecruitsID=<%#((System.Xml.XmlNode)Container.DataItem).Attributes["id"].Value %>" target ="news"><%#((System.Xml.XmlNode)Container.DataItem).InnerText%></a>
                    </div>
         
                    
                       </ItemTemplate>
                     </asp:Repeater>
                </li>
                
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>

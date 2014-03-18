<%@ Page Title="数字创新加油站" Language="C#" MasterPageFile="~/Master/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="_index" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
    <!-- 控制导航颜色变化  -->
    <script type="text/javascript">
    $(function(){
        $("a:eq(1)").css({ "color": "#E65D06" });
        });
        
    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <form id="form1" runat="server">
    <!-- banner-->
      <div id="bannerWrap">
     <div id="bannerWrap_top">
     </div>
  <div id="navigator"> <img src="<%=picture[0]%>" alt="Screenshot 1" width="950" height="296" /></div>
  </div>
    <!-- 
     <div class="mod" id="mod_album" xmlns="">
  
    <div id="m_album" class="modbox">
      <div style="" class="image"  id="imgArea">
        <center id="picdiv">
        </center>
      </div>
       <div class="modhead">
      <span class="phpage"><a href="#" onclick="playControl();return false;"><img border="0" align="absmiddle" src="images/icon/i_ph_stop.gif" title="暂停" onmouseover="m_over(this);" onmouseout="m_out(this);" id="playControlImg"/></a> <a href="#" onclick="previous();return false;"><img border="0" align="absmiddle" src="images/icon/i_ph_prev.gif" title="上一张" alt="上一张" onmouseover="this.src='images/icon/i_ph_prev_o.gif'" onmouseout="this.src='images/icon/i_ph_prev.gif'"/></a> <a href="#" onclick="next();return false;"><img border="0" align="absmiddle" src="images/icon/i_ph_next.gif" title="下一张" alt="下一张" onmouseover="this.src='images/icon/i_ph_next_o.gif'" onmouseout="this.src='images/icon/i_ph_next.gif'"/></a></span>
    </div>
    </div>
   
 <script language="javascript">
        var nowid = 0; 													// 当前相片的id 默认为0 显示的是第一张
        var pNum = 0; 													// 相片的数量 默认为0
        var time = 3000; 												// 自动播放的时间间隔 3秒
        var Starting;
        var isPlaying = true; 											// 默认为自动播放
        var playControlImg = document.getElementById("playControlImg"); 	// 得到控制元素
        var picdiv = document.getElementById("picdiv");
       					// 得到用于显示图片的Html元素
        var photobg = ['<%=picture1[0]%>', '<%=picture1[1]%>', '<%=picture1[2]%>'];
         var photoIndexs = ['<%=picture[0]%>', '<%=picture[1]%>', '<%=picture[2]%>']; 	
     

       
       		// 相册数组				
        pNum = photoIndexs.length; 										// 得到相册中的相片数量				
        if (0 != pNum) {														// 如果像册中存在相片
            document.getElementById("imgArea").style.display = ''; 		// 显示imgArea元素
            picdiv.innerHTML = '<div id="bannerWrap" style="background-image: url('+photobg[0]+');"><div id="navigator"><img src="' + photoIndexs[0] + '" border="0" width="950" height="296" /></div></div>'; // 显示当前图片
            Starting = setInterval('rockPhoto()', time); 				// 定时调用rockPhoto函数用来实现相片的定时自动播放
        }

        function m_over(ele) {												// 当鼠标在某个元素上的时候执行的函数 用来实现按钮样式的改变
            if (isPlaying) ele.src = "images/icon/i_ph_stop_o.gif";
            else ele.src = "../images/icon/i_ph_play_o.gif";
        }
        function m_out(ele) {												// 当鼠标离开某个元素时候执行的函数 用来实现按钮样式的改变
            if (isPlaying) ele.src = "images/icon/i_ph_stop.gif";
            else ele.src = "images/icon/i_ph_play.gif";
        }
        function playControl()												// 控制自动播放的函数
        {
            clearInterval(Starting);
            if (isPlaying)													// 正在播放
            {
                isPlaying = false; 										// 标记停止
                playControlImg.src = "images/icon/i_ph_play.gif";
                playControlImg.title = "播放";
            }
            else															// 否则
            {
                Starting = setInterval('rockPhoto()', time); 				// 开始自动播放
                isPlaying = true; 											// 标记开始
                playControlImg.src = "images/icon/i_ph_stop.gif";
                playControlImg.title = "暂停";
            }
        }
        function rockPhoto()												// 实现相片的定时自动播放函数
        {
            nowid++; 													// 当前相片标记自动加1
            if (nowid >= pNum) { nowid = 0; } 								// 如果相片标记数大于等于相册中相片的数量 则初始相片标记为0
            picdiv.innerHTML = '<div id="bannerWrap" style="background-image: url(' + photobg[nowid] + ');"><div id="navigator"><img src="' + photoIndexs[nowid] + '" border="0" width="950" height="296" /></div></div>'; //显示当前图片
        }
        function next()													// 下一张相片
        {
            rockPhoto();
        }
        function previous()												// 上一张相片
        {
            nowid--; 													// 当前相片标记自动减1
            if (nowid < 0) nowid = pNum - 1; 									// 如果相片标记数小于0 则初始相片标记为相册中相片数量减1		
            picdiv.innerHTML = '<div id="bannerWrap"  style="background-image: url(' + photobg[nowid] + ');"><div id="navigator"><img src="' + photoIndexs[nowid] + '" border="0" width="950" height="296" /></div></div>';
        }
</script>
 -->
 </div>

 

  <!-- end of banner-->

  <div id="content">
     <div id="column_left">

       <div class="tittle_02"><img src="<%=buttonImage[10]%>"  alt="" width="16" height="16" />&nbsp;<%=titleName[0] %><span class="more" style="left: 215px"> <a href="<%=titleUrl[0] %>">查看更多…</a> &nbsp;</span>    </div>


      <div class="content1">

          <table width="100%" border="0" cellspacing="10" cellpadding="0">

 
<!-- 修改 -->
        <asp:Repeater ID="News" runat="server" >
			          
                    <ItemTemplate>
                    <tr>

                        <td scope="col" width="660px"><a href="news/NewsContent.aspx?newsId=<%# Eval("NewsID")%>"><li style="position: relative; right: 15px;margin-left:6px;"><%# Eval("NewsTitle").ToString().Length > 10 ? Eval("NewsTitle").ToString().Substring(0, 10) + " … " : Eval("NewsTitle")%></li>

                            </a></td>

                        <td scope="col" width="100px" style="color:Gray"><%# ((System.DateTime)Eval("NewsDate")).ToString("yyyy.MM.dd")%></td6

                    </tr>
                     
                    </ItemTemplate>
                </asp:Repeater> 

</table>

       </div>              

     </div>

      <div id="column_middle" style="position: relative; left: 18px">
      <div class="tittle_02"> <img src="<%=buttonImage[12]%>" alt="" width="16" height="16" />&nbsp;<%=titleName[2] %>



	  <span class="more" style="left: 210px">   <a href="<%=titleUrl[2] %>">查看更多…</a>&nbsp;   </span>       </div>

       <div class="content1">   

              <table width="100%" border="0" cellspacing="10" cellpadding="0">

          <asp:Repeater ID="OilProject" runat="server" >
			          
                    <ItemTemplate>
                    <tr>

                      <td scope="col" width="500px"><a href="oilStation/OilProjectContent.aspx?OilProjectID=<%# Eval("OilProjectID")%>" ><li style="position: relative; right: 15px;margin-left:6px;"><%# Eval("OilProjectTitle").ToString().Length > 10 ? Eval("OilProjectTitle").ToString().Substring(0, 10) + " … " : Eval("OilProjectTitle")%></li>

                            </a></td>

                        <td scope="col" width="100px" style="color:Gray"><%# ((System.DateTime)Eval("OilProjectDate")).ToString("yyyy.MM.dd")%></td>

                    </tr>
                   
                    </ItemTemplate>
                </asp:Repeater>
              
</table>
       </div>
       
   </div>

		<div id="column_right">
         <div class="tittle_02">  <img src="<%=buttonImage[11]%>" alt="" width="16" height="16" />&nbsp;<%=titleName[1] %>

        <span class="more" style="left: 206px"> <a href="<%=titleUrl[1] %>">查看更多…</a>&nbsp;   
             </span>  </div>

        <div class="content1">

          <table width="100%" border="0" cellspacing="10" cellpadding="0">
          
          <asp:Repeater ID="Plans" runat="server" >
			          
                    <ItemTemplate>
                    <tr>

                        <td scope="col" width="500px"><a href="oilStation/planContent.aspx?StationPlanID=<%# Eval("StationPlanID")%>"><li style="position: relative; right: 15px;margin-left:6px;"><%# Eval("PlanName").ToString().Length > 10 ? Eval("PlanName").ToString().Substring(0, 10) + " … " : Eval("PlanName")%></li>

                            </a></td>

                        <td scope="col" width="110px" style="color:Gray"><%# ((System.DateTime)Eval("PlanDate")).ToString("yyyy.MM.dd")%></td>

                    </tr>
                     
                    </ItemTemplate>
                </asp:Repeater>

</table>

   	    </div>

       

     </div>

</div>
    </form>
</asp:Content>


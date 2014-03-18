<%@ Page Title="编辑常用问题" Language="C#"  ValidateRequest="false"  MasterPageFile="~/Master/AdminConsole.master" AutoEventWireup="true" CodeFile="FAQEditor.aspx.cs" Inherits="admin_FAQ" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
<style>
#Button2
{
    text-align:center;
}
.all
{
    width:630px;
}
</style>
<script>
     $(function () {
          $("#5").show();
         $("#05").html("-");
         $("#53").addClass("present");
         $("#a5").toggle(function (e) {
             e.preventDefault();
             $(this).next().hide();
             $(this).children(".jia").html("+");
         }, function (e) {
             e.preventDefault();
             $(this).next().show();
             $(this).children(".jia").html("-");
         });
     });
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
<div class="all">
    <div id="news-editor-bg">
    </div>
    <table id="news-editor-table">
        <tr>
            <td>
                问题:
            </td>
            <td>
                <asp:TextBox ID="Question" runat="server" Width="500px" Height="100px" meta:resourcekey="QuestionResource1"></asp:TextBox>
            </td>
        </tr>   
        <tr>
        <td>解答：</td>
           <td>
                <asp:TextBox ID="Answer" runat="server" Width="500" Height="100px" meta:resourcekey="AnswerResource1"></asp:TextBox>
            </td>
        </tr>
    </table>

    <div id="Button2">
   <asp:Button ID="Button2" runat="server" OnClick="btnSubmit_Click" Text="提交" meta:resourcekey="btnSubmitResource1" />
   </div>
   </div>
</asp:Content>




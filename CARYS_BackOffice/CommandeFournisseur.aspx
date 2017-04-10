<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="CommandeFournisseur.aspx.cs" Inherits="CARYS_BackOffice.CommandeFournisseur" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1 class="text-center">Gestion commande fournisseur</h1>
        <hr />
    </div>
    <div class="row">
        <div class="col-lg-6">
            <div class="form-group">
                <asp:EntityDataSource ID="EdsFournisseur" runat="server" ConnectionString="name=BddEntities" DefaultContainerName="BddEntities" EnableFlattening="False" EntitySetName="Fournisseurs"></asp:EntityDataSource>
                <label for="DropDownListFournisseur">Choix du fournisseur : </label>
                <asp:DropDownList ID="DropDownListFournisseur" CssClass="form-control" runat="server" DataSourceID="EdsFournisseur" DataTextField="NomFournisseur" DataValueField="IdFournisseur" OnSelectedIndexChanged="DropDownListFournisseur_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-6">
            <asp:Button ID="BtnNouvelleCommande" CssClass="btn btn-primary btn-block" runat="server" Text="Nouvelle commande" OnClick="BtnNouvelleCommandeFournisseur_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-6">
            <label for="TextQuantite">Quantite : </label>
            <asp:TextBox ID="TextQuantite" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-6">
            <asp:Button ID="BtnAddArticle" CssClass="btn btn-primary btn-block" runat="server" Text="Ajouter article" OnClick="BtnAddArticle_Click" />
        </div>
    </div>
    <br />
    <div class="row">
        <asp:GridView ID="GridViewArticle" GridLines="None" CssClass="table table-responsive" runat="server" AutoGenerateColumns="true" OnSelectedIndexChanged="GridViewArticle_SelectedIndexChanged">
            <%--<Columns>
                <asp:BoundField DataField="Reference" HeaderText="Ref"></asp:BoundField>
                <asp:BoundField DataField="LibelleArticle" HeaderText="Libell&#233;"></asp:BoundField>
                <asp:BoundField DataField="PrixAchat" HeaderText="Prix"></asp:BoundField>
                <asp:CommandField ShowSelectButton="True" HeaderText="Action"></asp:CommandField>
            </Columns>--%>
        </asp:GridView>
    </div>
    <br />
    <div class="row">
        <asp:HiddenField ID="HiddenNumeroCommande" runat="server" />
        <asp:GridView ID="GridViewCommande" CssClass="table table-responsive" GridLines="None" runat="server"></asp:GridView>
    </div>
</asp:Content>

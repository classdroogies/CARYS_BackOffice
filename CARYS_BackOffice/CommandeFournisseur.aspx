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
                <asp:DropDownList ID="DropDownListFournisseur" CssClass="form-control" runat="server" DataSourceID="EdsFournisseur" DataTextField="NomFournisseur" DataValueField="IdFournisseur" OnSelectedIndexChanged="DropDownListFournisseur_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-6">
            <asp:Button ID="BtnValiderFournisseur" CssClass="btn btn-primary btn-block" runat="server" Text="Nouvelle commande" OnClick="BtnNouvelleCommandeFournisseur_Click" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-4">
            <asp:EntityDataSource ID="EdsArticle" runat="server" ConnectionString="name=BddEntities" DefaultContainerName="BddEntities" EnableFlattening="False" EntitySetName="Articles" EntityTypeFilter="Article" OnQueryCreated="EdsArticle_QueryCreated"></asp:EntityDataSource>
            <label for="DropDownListArticle">Choix article : </label>
            <asp:DropDownList ID="DropDownListArticle" CssClass="form-control" runat="server" DataSourceID="EdsArticle" DataTextField="LibelleArticle" DataValueField="Reference"></asp:DropDownList>
        </div>
        <div class="col-lg-2">
            <label for="TextQuantite">Quantite : </label>
            <asp:TextBox ID="TextQuantite" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="col-lg-6">
            <asp:Button ID="BtnAddArticle" CssClass="btn btn-primary btn-block" runat="server" Text="Ajouter article" OnClick="BtnAddArticle_Click" />
        </div>
    </div>
    <br />
    <div class="row">
        <asp:HiddenField ID="HiddenNumeroCommande" runat="server" />
        <asp:GridView ID="GridViewCommande" CssClass="table table-responsive" GridLines="None" runat="server"></asp:GridView>
    </div>
</asp:Content>

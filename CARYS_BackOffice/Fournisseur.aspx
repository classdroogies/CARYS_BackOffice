<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="Fournisseur.aspx.cs" Inherits="CARYS_BackOffice.Fournisseur" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h1 class="text-center">Gestion des fournisseurs</h1>
        <hr />
    </div>
    <div class="jumbotron">
        <h2>Ajout d'un nouveau fournisseur :</h2>
        <br />
        <asp:EntityDataSource ID="EdsAddFournisseur" runat="server" ConnectionString="name=BddEntities" DefaultContainerName="BddEntities" EnableFlattening="False" EnableInsert="True" EntitySetName="Fournisseurs"></asp:EntityDataSource>
        <asp:DetailsView ID="DvAddFournisseur" CssClass="table" GridLines="None" runat="server" AutoGenerateRows="False" DataKeyNames="IdFournisseur" DataSourceID="EdsAddFournisseur" DefaultMode="Insert" OnItemInserted="DvAddFournisseur_ItemInserted">
            <Fields>
                <asp:BoundField DataField="NomFournisseur" HeaderText="Nom Fournisseur" SortExpression="NomFournisseur"></asp:BoundField>
                <asp:CommandField ShowInsertButton="True">
                    <ControlStyle CssClass="btn btn-primary btn-block"></ControlStyle>
                </asp:CommandField>
            </Fields>
        </asp:DetailsView>
    </div>
    <div class="row">
        <asp:EntityDataSource ID="EdsListeFournisseurs" runat="server" ConnectionString="name=BddEntities" DefaultContainerName="BddEntities" EnableDelete="True" EnableFlattening="False" EnableUpdate="True" EntitySetName="Fournisseurs"></asp:EntityDataSource>
        <asp:GridView ID="GridViewFournisseurs" CssClass="table table-responsive" runat="server" DataSourceID="EdsListeFournisseurs" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" GridLines="None">
            <Columns>
                <asp:BoundField DataField="IdFournisseur" HeaderText="Id" ReadOnly="True" SortExpression="IdFournisseur"></asp:BoundField>
                <asp:BoundField DataField="NomFournisseur" HeaderText="Nom Fournisseur" SortExpression="NomFournisseur"></asp:BoundField>
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" HeaderText="Action">
                    <ControlStyle CssClass="btn btn-primary"></ControlStyle>
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

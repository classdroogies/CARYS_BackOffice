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
                <asp:DropDownList ID="DropDownListFournisseur" AppendDataBoundItems="true" CssClass="form-control" runat="server" DataSourceID="EdsFournisseur" DataTextField="NomFournisseur" DataValueField="IdFournisseur" OnSelectedIndexChanged="DropDownListFournisseur_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Selected="True" Text="Tous" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-6">
            <asp:Button ID="BtnNouvelleCommande" CssClass="btn btn-primary btn-block" runat="server" Text="Nouvelle commande" OnClick="BtnNouvelleCommandeFournisseur_Click" />
        </div>
    </div>
    <br />
    <div class="row">
        <asp:GridView ID="GridViewCommande" CssClass="table table-responsive" GridLines="None" runat="server"></asp:GridView>
    </div>
    <div class="row">
        <asp:ListView ID="ListViewArticles" GroupItemCount="4" runat="server">
            <LayoutTemplate>
                <div class="row">
                    <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
            </LayoutTemplate>
            <GroupTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </GroupTemplate>
            <ItemTemplate>
                <div class="col-lg-3">
                    <h3>
                        <asp:Label ID="LblLibelle" Text='<%# Eval("LibelleArticle") %>' runat="server" />
                    </h3>
                    <p>
                        <strong>Prix : </strong>
                        <asp:Label ID="LblPrix" Text='<%# Eval("PrixFournisseur") %>' runat="server" />
                        &euro; HT
                    </p>
                    <p><strong>Quantité en stock : </strong><%# Eval("QuantiteStock") %></p>
                    <p><strong>Seuil : </strong><%# Eval("SeuilStock") %></p>
                    <div class="row">
                        <div class="col-lg-5">
                            <asp:TextBox ID="TxtQuantite" placeholder="Quantité" CssClass="form-control text-center" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-lg-7">
                            <asp:LinkButton OnClick="BtnAddArticle_Click" CommandArgument='<%# Eval("Reference") %>' CommandName="Add" CssClass="btn btn-primary btn-block" runat="server">Commander</span></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div>Aucun article</div>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.Master" AutoEventWireup="true" CodeBehind="CommandeFournisseur.aspx.cs" Inherits="CARYS_BackOffice.CommandeFournisseur" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="ContentMain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-2"></div>
        <div class="col-lg-8">
            <h1 class="text-center">Gestion commande fournisseur</h1>
        </div>
        <div class="col-lg-2">
            <!-- Single button -->
            <div id="panier" class="btn-group" runat="server">
                <button type="button" class="btn btn-primary btn-lg dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Panier <span class="badge"><%= GridViewCommande.Rows.Count %></span>  <span class="caret"></span>
                </button>
                <div id="panierMenu" class="dropdown-menu dropdown-menu-right">
                    <asp:GridView ID="GridViewCommande" DataKeyNames="Reference" CssClass="table table-responsive" ShowHeaderWhenEmpty="true" GridLines="None" runat="server" AutoGenerateColumns="False" OnRowDeleting="GridViewCommande_RowDeleting" OnRowEditing="GridViewCommande_RowEditing" OnRowUpdating="GridViewCommande_RowUpdating" OnRowCancelingEdit="GridViewCommande_RowCancelingEdit">
                        <Columns>
                            <asp:BoundField DataField="Reference" HeaderText="R&#233;f&#233;rence" ReadOnly="true" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="LibelleArticle" HeaderText="Libell&#233;" ReadOnly="true"></asp:BoundField>
                            <asp:TemplateField HeaderText="Quantit&#233;" HeaderStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:Label ID="LblQuantiteCommande" Text='<%# Eval("QuantiteCommandeFournisseur") %>' runat="server" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TxtQuantiteCommande" CssClass="form-control" Text='<%# Bind("QuantiteCommandeFournisseur") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prix" HeaderStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label ID="LblPrixFournisseurCommande" Text='<%# String.Format("{0:0.00}", Eval("PrixFournisseur")) %>' runat="server" />
                                    &euro;
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True"></asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True"></asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <hr />
        <asp:Label ID="LblSuccess" Text="" CssClass="alert alert-success btn-block" Visible="false" runat="server" />
        <asp:Label ID="LblError" Text="" CssClass="alert alert-danger btn-block" Visible="false" runat="server" />
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
            <div id="GroupBtnCommande" class="row" runat="server">
                <div class="col-lg-6">
                    <asp:Button ID="BtnValiderCommande" CssClass="btn btn-success btn-block" runat="server" Text="Valider commande" OnClick="BtnValiderCommande_Click" />
                </div>
                <div class="col-lg-6">
                    <asp:Button ID="BtnAnnulerCommande" CssClass="btn btn-danger btn-block" runat="server" Text="Annuler commande" OnClick="BtnAnnulerCommande_Click" />
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <asp:ListView ID="ListViewArticles" GroupItemCount="3" runat="server" OnLoad="ListViewArticles_Load" OnPagePropertiesChanging="ListViewArticles_PagePropertiesChanging">
            <LayoutTemplate>
                <div class="row">
                    <asp:PlaceHolder ID="groupPlaceholder" runat="server"></asp:PlaceHolder>
                </div>
            </LayoutTemplate>
            <GroupTemplate>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            </GroupTemplate>
            <ItemTemplate>
                <div class="col-lg-3 boxArticle">
                    <div class="article">
                        <h4>
                            <asp:Label ID="LblLibelle" Text='<%# Eval("LibelleArticle") %>' runat="server" />
                        </h4>
                        <p>
                            <strong>Prix : </strong>
                            <asp:Label ID="LblPrix" Text='<%# String.Format("{0:0.00}", Eval("PrixFournisseur")) %>' runat="server" />
                            &euro;
                        </p>
                        <p><strong>Quantité en stock : </strong><%# Eval("QuantiteStock") %></p>
                        <p><strong>Seuil : </strong><%# Eval("SeuilStock") %></p>
                        <p><strong>Genre : </strong><%# Eval("LibelleGenre") %></p>
                        <p><strong>Categorie : </strong><%# Eval("LibelleCategorie") %></p>
                        <div class="row commandeArticle">
                            <div class="col-lg-5">
                                <asp:TextBox ID="TxtQuantite" placeholder="Quantité" CssClass="form-control text-center" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-lg-7">
                                <asp:LinkButton OnClick="BtnAddArticle_Click" CommandArgument='<%# Eval("Reference") %>' CommandName="Add" CssClass="btn btn-primary btn-block" runat="server">Ajouter</span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
            <EmptyDataTemplate>
                <div>Aucun article</div>
            </EmptyDataTemplate>
        </asp:ListView>
    </div>
    <div class="row">
        <asp:DataPager ID="DataPager" PagedControlID="ListViewArticles" runat="server" PageSize="20">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn btn-primary" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                <asp:NumericPagerField></asp:NumericPagerField>
                <asp:NextPreviousPagerField ButtonType="Button" ButtonCssClass="btn btn-primary" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
            </Fields>
        </asp:DataPager>
    </div>
</asp:Content>

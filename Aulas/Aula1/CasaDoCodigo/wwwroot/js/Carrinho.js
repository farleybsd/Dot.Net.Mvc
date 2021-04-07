class Carrinho
{
    clickIncremento(btn) {

        let data = this.GetData(btn);
        data.Quantidade++;
        this.PostQuantidade(data);
       
    }

    clickDecremento(btn) {
        let data = this.GetData(btn);
        data.Quantidade--;
        this.PostQuantidade(data);
    }

    GetData(elemento) {
        var linhaDoItem = $(elemento).parents('[item-id]'); // pegando elemento a cima do botao no DOM
        var itemId = $(linhaDoItem).attr('item-id');
        var novaQtd = $(linhaDoItem).find('input').val() // pegando elemento a abaixo do botao no DOM

        return {
            Id: itemId,
            Quantidade: novaQtd
        };
    }

    PostQuantidade(data) {

        let token = $('[name=__RequestVerificationToken]').val();

        let headers = {};
        headers['RequestVerificationToken'] = token;

        $.ajax({
            url: '/pedido/UpdateQuantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: headers
        }).done(function (response) {

            let itemPedido = response.itemPedido;
            let linhaDoItem = $('[item-id=' + itemPedido.id + ']')
            linhaDoItem.find('input').val(itemPedido.quantidade);
            linhaDoItem.find('[subtotal]').html((itemPedido.subtotal).duasCasas());
            let carrinhoViewModel = response.carrinhoViewModel;
            $('[numero-itens]').html('Total: ' + carrinhoViewModel.itens.length + ' itens');
            $('[total]').html((carrinhoViewModel.total).duasCasas());


            if (itemPedido.quantidade == 0) {
                linhaDoItem.remove();
            }
            //location.reload() // Atualiza o HTML
        });
    }

    UpdateQuantidade(input) {
        let data = this.GetData(input);
        this.PostQuantidade(data);
    }
}

var carrinho = new Carrinho();

Number.prototype.duasCasas = function () {
    return this.toFixed(2).replace('.',',');
}
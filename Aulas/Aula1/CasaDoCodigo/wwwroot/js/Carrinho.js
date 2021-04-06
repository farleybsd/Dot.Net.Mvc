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
        $.ajax({
            url: '/pedido/UpdateQuantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data)
        }).done(function (response) {

            //location.reload() // Atualiza o HTML
        });
    }

    UpdateQuantidade(input) {
        let data = this.GetData(input);
        this.PostQuantidade(data);
    }
}

var carrinho = new Carrinho();

while (sairDaAplicacao !== true) {
    let nomeCliente = prompt("Digite seu nome:");
    console.log("Olá, " + nomeCliente + " seja bem vindo ao HuFood!!");
    alert("Olá, " + nomeCliente + " seja bem vindo ao HuFood!!");
    
    let Produtos = [
        {
            nome: "Hamburguer",
            valor: 12.00
        },
        {
            nome: "Hot-Dog",
            valor: 9.00
        },
        {
            nome: "Pizza",
            valor: 21.00
        },
        {
            nome: "Refri",
            valor: 3.00
        },
    ];

    let opcao = prompt("Opção 1 - Cardapio\nOpção 2 - Login\nOpção 3 - Cadastro\n\nDigite o numero da opção escolhida, para prosseguir:");
    console.log("Opção 1 - Cardapio\nOpção 2 - Login\nOpção 3 - Cadastro\n\nDigite o numero da opção escolhida, para prosseguir:");
    switch (parseInt(opcao)) {
        case 1:
            let todosOsProdutos;
            let quantidadeProduto = 0;
            for (let i = 0; i < Produtos.length; i++) {
                quantidadeProduto = i;
                todosOsProdutos += [((i+1) + "." + " " + Produtos[i].nome + "\n")];
            }
            console.log(quantidadeProduto);
            console.log(todosOsProdutos);
            let produtoPedido = prompt(todosOsProdutos + "\n" + "Digite o numero de um produto do cardapio, ou digite sair!!");
            if(produtoPedido == "sair") 
            {
                return;
            }
            let respostaSegundoPedido = prompt("Quer fazer outro pedido??\nY/N");
            console.log("Quer fazer outro pedido??\nY/N");
            if(respostaSegundoPedido.toLowerCase == "y")
            {
                return;
            }
            break

        case 2:

            break

        case 3:

            break

        default:
            alert("Opção não listada!!")
    }
}
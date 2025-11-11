// Aguarda o HTML ser completamente carregado antes de executar o script
document.addEventListener('DOMContentLoaded', () => {

    const searchInput = document.getElementById('searchInput');
    const searchButton = document.getElementById('searchButton');
    const productListDiv = document.getElementById('productList');

    // ATENÇÃO: A URL base da sua API. Verifique a porta!
    // Esta é a URL para chamar os endpoints, NÃO para abrir a página.
    // Como o JS está sendo servido pela mesma origem, podemos usar um caminho relativo.
    const apiUrl = '/api/produtos';

    // Função para buscar os produtos na API e exibi-los
    async function fetchAndDisplayProducts(searchTerm = '') {
        let url = apiUrl;
        if (searchTerm) {
            // Adiciona o parâmetro de busca na URL
            url += `?nome=${encodeURIComponent(searchTerm)}`;
        }

        try {
            productListDiv.innerHTML = '<p>Carregando...</p>'; // Feedback para o usuário

            const response = await fetch(url);
            if (!response.ok) {
                throw new Error(`Erro na API: ${response.statusText}`);
            }
            const products = await response.json();

            // Limpa a lista antes de adicionar os novos itens
            productListDiv.innerHTML = '';

            if (products.length === 0) {
                productListDiv.innerHTML = '<p>Nenhum produto encontrado.</p>';
                return;
            }

            // Cria um elemento HTML para cada produto e adiciona na lista
            products.forEach(product => {
                const productElement = document.createElement('div');
                productElement.innerHTML = `
                    <h3>${product.nome}</h3>
                    <p><strong>ID:</strong> ${product.id}</p>
                    <p><strong>Quantidade:</strong> ${product.quantidade}</p>
                    <p><strong>Preço:</strong> R$ ${product.preco.toFixed(2)}</p>
                `;
                productListDiv.appendChild(productElement);
            });

        } catch (error) {
            productListDiv.innerHTML = `<p style="color: red;">Erro ao carregar produtos: ${error.message}</p>`;
            console.error('Falha ao buscar produtos:', error);
        }
    }

    // Evento de clique no botão de busca
    searchButton.addEventListener('click', () => {
        fetchAndDisplayProducts(searchInput.value);
    });

    // Permite buscar pressionando Enter no campo de busca
    searchInput.addEventListener('keyup', (event) => {
        if (event.key === 'Enter') {
            fetchAndDisplayProducts(searchInput.value);
        }
    });


    // Carrega todos os produtos quando a página abre pela primeira vez
    fetchAndDisplayProducts();
});

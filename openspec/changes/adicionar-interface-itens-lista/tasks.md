# Tasks

## 1. Conectar os dados à apresentação

- [ ] 1.1 Completar em `ServicoItemLista` as consultas de opções de produtos e detalhes por id, preservando alterações locais existentes; verificar que os DTOs fornecem categoria, unidade, preço, lista e subtotal corretos e que id inexistente retorna falha controlada.
- [ ] 1.2 Implementar `ItemListaProfile`, incluindo os parâmetros de construtor e a correspondência `ListaComprasNoem`/`ListaComprasNome`, e registrar `ServicoItemLista`; verificar os mapeamentos com instâncias representativas e executar `dotnet build` sem erros.

## 2. Entregar a consulta dos itens

- [ ] 2.1 Implementar GET `ItemLista/Listar` e a view com nome, status, produtos, categorias, unidades, quantidades, subtotais, resumo e estado vazio; verificar com duas listas que somente os itens selecionados aparecem e que 2 × R$ 15,00 + 1 × R$ 6,00 resulta em 2 itens e R$ 36,00.
- [ ] 2.2 Adicionar “Ver itens” nos cartões de Minhas listas e manter essa seção ativa no layout durante a navegação de itens; verificar a entrada em cada lista, o retorno e a preservação das ações existentes.
- [ ] 2.3 Aplicar estilos do módulo para linhas em desktop e cartões em telas pequenas; verificar largura de 375 pixels sem rolagem horizontal da página, nomes longos, rótulos, foco por teclado e estado vazio com 0 itens/R$ 0,00.

## 3. Entregar o cadastro separado

- [ ] 3.1 Implementar GET/POST `Cadastrar`, view com lista identificada, produto e quantidade, antiforgery, validação e opções recarregadas após falha; verificar produto vazio/inexistente, duplicidade, quantidade vazia/não numérica/zero/negativa e conservação dos valores enviados.
- [ ] 3.2 Mostrar categoria, unidade e preço da seleção com script local e fallback nas opções; verificar troca de produto, seleção restaurada após erro e estado sem produtos com orientação, link de cadastro e envio desabilitado.
- [ ] 3.3 Implementar sucesso com mensagem e redirecionamento para os itens da mesma lista e Cancelar sem gravação; verificar persistência ao reabrir, atualização do total, refresh sem duplicidade e inclusão fracionária de 1,5 sem mudança de escala.

## 4. Entregar a remoção com confirmação

- [ ] 4.1 Implementar GET de confirmação e view com produto, lista, quantidade e subtotal; verificar que abrir e cancelar não removem o item e retornam à lista correta.
- [ ] 4.2 Implementar POST de remoção com antiforgery, nova consulta do item e validação da lista real; verificar mensagem de sucesso, totais recalculados, remoção do último item e ausência de alterações em outra lista quando houver identificadores incompatíveis.
- [ ] 4.3 Tratar listas e itens inexistentes nos fluxos de consulta, inclusão e remoção; verificar mensagens e retornos definidos na especificação, inclusive item desaparecido entre a confirmação e o envio, sem erro de servidor.

## 5. Validar a integração completa

- [ ] 5.1 Executar `dotnet build` e percorrer Minhas listas → Ver itens → Adicionar → retorno → Remover → retorno com dados descartáveis isolados; verificar persistência e totais tanto na página de itens quanto nos cartões de Minhas listas, preservando o arquivo de dados de uso real.
- [ ] 5.2 Revisar as três páginas no desktop e em 375 pixels, com teclado, mensagens de erro/sucesso e navegação de volta; verificar que Minhas listas e cadastro de produtos continuam legíveis e funcionais após os ajustes compartilhados e registrar os resultados da validação na entrega.

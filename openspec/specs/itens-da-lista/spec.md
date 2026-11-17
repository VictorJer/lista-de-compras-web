# itens-da-lista Specification

## Purpose

Permitir que a pessoa organize os produtos de cada lista de compras por meio de páginas de consulta, inclusão e remoção, acompanhando quantidades e valores estimados.

## Requirements

### Requirement: Acessar itens no contexto de uma lista
O sistema SHALL oferecer “Ver itens” em cada cartão de Minhas listas e abrir somente os itens da lista selecionada, identificando seu nome e status e permitindo voltar a Minhas listas.

#### Scenario: Abrir uma lista entre várias
- **GIVEN** existem duas listas com produtos diferentes
- **WHEN** a pessoa aciona “Ver itens” em uma delas
- **THEN** a página mostra o nome, status e somente os itens da lista escolhida
- **AND** oferece acesso a Minhas listas e à página de adicionar item

### Requirement: Exibir itens e totais estimados
O sistema SHALL exibir produto, categoria identificada por nome e cor, unidade, preço estimado unitário, quantidade e subtotal por item, além do número de itens distintos e da soma dos subtotais da lista. Valores monetários SHALL usar reais no formato brasileiro.

#### Scenario: Consultar valores calculados
- **GIVEN** uma lista contém 2 unidades de um produto de R$ 15,00 e 1 unidade de outro de R$ 6,00
- **WHEN** a pessoa consulta seus itens
- **THEN** vê subtotais de R$ 30,00 e R$ 6,00, 2 itens e total estimado de R$ 36,00

#### Scenario: Lista vazia
- **WHEN** a pessoa abre uma lista sem itens
- **THEN** vê 0 itens, total de R$ 0,00 e uma orientação com ação para adicionar o primeiro item

### Requirement: Adicionar item em página separada
O sistema SHALL oferecer uma página separada para selecionar um produto cadastrado e informar quantidade positiva, mantendo a lista de destino identificada. A página SHALL mostrar categoria, unidade e preço estimado do produto selecionado e permitir cancelar voltando aos itens sem gravar.

#### Scenario: Selecionar produto
- **WHEN** a pessoa seleciona ou troca o produto no cadastro de item
- **THEN** a categoria, unidade e preço exibidos correspondem à seleção atual

#### Scenario: Cancelar inclusão
- **WHEN** a pessoa cancela o cadastro
- **THEN** retorna aos itens da mesma lista sem criar nenhum item

#### Scenario: Não existem produtos cadastrados
- **WHEN** a pessoa abre o cadastro de item sem produtos disponíveis no catálogo
- **THEN** a página informa a necessidade de cadastrar um produto, oferece acesso ao cadastro de produtos e impede o envio de uma inclusão vazia

### Requirement: Retornar aos itens após inclusão
O sistema SHALL persistir uma inclusão válida e redirecionar para os itens da mesma lista, exibindo mensagem de sucesso, o novo item e totais recalculados.

#### Scenario: Inclusão válida
- **WHEN** a pessoa envia um produto ainda ausente na lista com quantidade válida
- **THEN** retorna aos itens da lista de origem com mensagem de sucesso e valores atualizados
- **AND** atualizar essa página não repete a inclusão
- **AND** reabrir a lista mantém o item salvo

### Requirement: Apresentar erros de inclusão
O sistema SHALL rejeitar produto ausente ou inválido, quantidade inválida e produto duplicado na mesma lista. Em falhas de validação, SHALL manter o formulário, os valores informados e as opções disponíveis, exibindo mensagem associada ao campo ou ao formulário sem modificar a lista.

#### Scenario: Produto duplicado
- **WHEN** a pessoa tenta adicionar um produto já presente na lista
- **THEN** permanece no cadastro com aviso de duplicidade e nenhum item adicional é criado

#### Scenario: Produto ou quantidade inválidos
- **WHEN** a pessoa envia produto vazio, inexistente, quantidade vazia, não numérica, zero ou negativa
- **THEN** o formulário mostra o erro correspondente e conserva os dados enviados para correção

#### Scenario: Quantidade decimal
- **WHEN** a pessoa envia uma quantidade fracionária válida no formato aceito e indicado pelo campo
- **THEN** a quantidade é preservada sem mudança de escala e o subtotal reflete esse valor

### Requirement: Confirmar remoção em página separada
O sistema SHALL oferecer “Remover” por item e abrir uma página de confirmação com lista, produto, quantidade e subtotal. Somente confirmar o envio SHALL remover o item; confirmar com sucesso ou cancelar SHALL retornar aos itens da mesma lista.

#### Scenario: Abrir e cancelar confirmação
- **WHEN** a pessoa abre a confirmação e cancela
- **THEN** nenhum item é removido e os itens da lista de origem são exibidos

#### Scenario: Confirmar remoção
- **WHEN** a pessoa confirma a remoção de um item existente
- **THEN** o item é removido e a pessoa retorna à lista de origem com mensagem de sucesso e totais atualizados
- **AND** remover o último item mostra o estado de lista vazia
- **AND** os itens de outras listas permanecem intactos

### Requirement: Tratar destinos inexistentes
O sistema SHALL tratar listas e itens inexistentes com mensagem compreensível e retorno seguro, sem falha de servidor nem alteração em outra lista. O destino de retorno de uma remoção SHALL corresponder à lista real do item.

#### Scenario: Lista inexistente
- **WHEN** uma página de itens ou inclusão é acessada com uma lista inexistente
- **THEN** a pessoa retorna a Minhas listas com mensagem de erro e nenhum dado é gravado

#### Scenario: Item removido anteriormente
- **WHEN** a pessoa tenta consultar ou confirmar a remoção de um item que não existe mais
- **THEN** recebe mensagem de erro e retorna aos itens se a lista de origem puder ser validada, ou a Minhas listas caso contrário

### Requirement: Manter identidade visual e acessibilidade
As páginas SHALL seguir a identidade Na Lista, manter Minhas listas indicada na navegação e permitir uso em desktop e celular sem perda de informações ou ações. Controles SHALL ter rótulos, foco visível e operação por teclado; mensagens e categorias SHALL ser compreensíveis sem depender apenas de cor.

#### Scenario: Usar no celular
- **WHEN** a pessoa abre as páginas em uma largura de 375 pixels
- **THEN** consegue ler os dados e alcançar os botões sem rolagem horizontal da página

#### Scenario: Usar teclado e receber feedback
- **WHEN** a pessoa navega pelo formulário e ações usando teclado
- **THEN** o foco permanece visível, os campos têm rótulos e mensagens de sucesso ou erro são expostas a tecnologias assistivas

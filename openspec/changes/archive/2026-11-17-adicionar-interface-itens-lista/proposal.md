# Proposal

## Why

O módulo de itens já possui domínio, persistência e parte dos serviços, mas ainda não oferece telas para organizar os produtos de uma lista. Completar esse fluxo permite usar a aplicação para montar compras e acompanhar seu valor estimado, mantendo a identidade visual Na Lista.

## What Changes

- Acrescentar a ação “Ver itens” nos cartões de Minhas listas.
- Criar uma página de itens vinculada à lista selecionada, com nome, status, quantidade de itens, total estimado, produtos, categorias, unidades, quantidades e subtotais.
- Criar uma página separada para adicionar produto e quantidade, exibindo categoria, unidade e preço do produto selecionado.
- Após adicionar, retornar aos itens da mesma lista com mensagem de sucesso e totais atualizados, conforme escolha do usuário.
- Criar uma página de confirmação de remoção e retornar aos itens após confirmar ou cancelar.
- Cobrir lista vazia, ausência de produtos, validações e registros inexistentes, com visual responsivo e acessível.
- Completar somente as conexões MVC, mapeamentos, consultas e registro de serviço necessários às telas, reaproveitando as regras existentes.
- Fora do escopo: editar quantidade de um item existente, marcar produtos como comprados, busca/filtros, novas regras para listas concluídas e mudanças de armazenamento.

## Capabilities

### New Capabilities

- `itens-da-lista`: navegação e gerenciamento visual dos itens de uma lista, incluindo inclusão, remoção, feedback e totais estimados.

### Modified Capabilities

Nenhuma. O projeto ainda não possui especificações principais registradas no OpenSpec.

## Impact

- `ModuloItemLista/Apresentacao`: controller e profile atualmente vazios, modelos existentes e novas views Razor.
- `ModuloItemLista/Aplicacao`: consultas de produtos e detalhes de item necessárias à interface e mapeamento dos DTOs existentes.
- `Compartilhado/Aplicacao/InjecaoDependencia.cs`: registro de `ServicoItemLista`.
- View de listagem de `ModuloListaCompras`, layout compartilhado e estilos em `wwwroot/css/site.css`; pequeno script local para os dados do produto selecionado.
- Sem novas dependências externas ou migração dos dados JSON. Preservar as alterações locais já existentes em `ServicoItemLista.cs`.

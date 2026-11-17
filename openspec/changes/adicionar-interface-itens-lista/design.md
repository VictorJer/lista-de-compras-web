# Design

## Context

Ver `proposal.md` para a motivação. A aplicação usa ASP.NET Core MVC em .NET 10, views por módulo, AutoMapper, FluentResults e persistência JSON. O módulo de itens já possui entidade, repositório registrado, DTOs, view models e serviço com inclusão, exclusão, listagem por lista e totais. Controller e profile estão vazios, não existem views e o serviço ainda não está registrado na injeção de dependências. Faltam consultas de opções de produtos e detalhes de item.

Há particularidades a considerar: os view models de itens estão no namespace de listas; os DTOs usam `ListaComprasNoem`, enquanto as views esperam `ListaComprasNome`; o layout não expõe uma seção de scripts. A implementação deverá preservar a alteração local preexistente em `ServicoItemLista.cs`. Não foi encontrado projeto de testes.

O design é necessário porque a entrega cruza itens, navegação de listas e componentes compartilhados.

## Goals / Non-Goals

**Goals:** integrar as três páginas ao MVC existente, manter os cálculos no servidor e reduzir alterações fora do módulo. Usar os contratos de comportamento de `specs/itens-da-lista/spec.md` como critérios de aceitação.

**Non-Goals:** introduzir SPA, API JSON, biblioteca de UI, migração de dados ou refatoração geral dos módulos. Não alterar as regras de status das listas: o serviço atual não impede inclusão ou remoção em listas concluídas e esta entrega preserva esse comportamento.

## Decisions

### 1. Páginas Razor e retorno por redirecionamento

Criar `Views/Listar.cshtml`, `Cadastrar.cshtml` e `Excluir.cshtml` em `ModuloItemLista/Apresentacao`. Usar `ItemListaController` com GET `Listar(listaComprasId)`, GET/POST `Cadastrar` e GET/POST `Excluir`. Sucesso em POST usa redirect para `Listar` da mesma lista e mensagem em TempData, seguindo o padrão de listas. GET de exclusão somente consulta.

Formulário embutido e modal foram descartados porque o usuário escolheu página separada. Permanecer no cadastro após salvar também foi descartado por escolha explícita do usuário.

### 2. Adaptar os contratos existentes

Registrar `ServicoItemLista` e completar consultas `SelecionarProdutos()` e `SelecionarPorId(id)` usando os repositórios já injetados e DTOs existentes. Implementar `ItemListaProfile` para itens, detalhes da lista, opções, cadastro e exclusão. Mapear explicitamente `ListaComprasNoem` para `ListaComprasNome` nos parâmetros de construtor dos records; evitar renomear contratos nesta entrega. Usar imports locais nas views para os namespaces existentes.

Manter preço e subtotal derivados do produto persistido. Não receber preços, nomes ou totais do navegador como fonte de dados. Não acessar repositórios diretamente pelas views ou pelo controller; a alternativa duplicaria regras e acoplaria apresentação à persistência.

### 3. Contexto e falhas

Validar a existência da lista antes de renderizar ou processar inclusão. Recarregar opções de produtos em erros de ModelState ou FluentResults, preservando valores enviados. Produto vazio deve ser rejeitado mesmo quando convertido em `Guid.Empty`. A quantidade mantém o contrato atual do view model, com mínimo de 0,01 e passo visual de 0,01; não ampliar a precisão nesta entrega.

Na remoção, consultar novamente o item no servidor e usar sua lista real para retorno. Se for transportado um identificador de lista junto com o item, verificar a correspondência antes de remover. Para registros desaparecidos, retornar à lista de origem apenas quando validada; caso contrário, usar Minhas listas. Proteger os dois POSTs com antiforgery, como no controller de listas.

### 4. Composição visual

Reaproveitar tokens, botões, cartões, badges e estados vazios de `site.css`. Na consulta, usar cabeçalho com volta, nome e status; resumo de itens/total; botão “Adicionar item”; itens em linhas no desktop e cartões empilhados no celular, sem duplicar controles acessíveis. Mostrar nome da categoria junto da cor e unidade junto da quantidade.

No cadastro, usar cartão centralizado como o cadastro de produtos, com nome da lista, select, resumo do produto, quantidade e ações Cancelar/Adicionar à lista. A confirmação mostra os dados do item e ações Cancelar/Remover. Manter Minhas listas ativa quando o controller for `ItemLista`, sem criar nova seção no menu. Acrescentar “Ver itens” aos cartões existentes e acomodar os demais botões em telas pequenas.

A alternativa de criar um estilo próprio para o módulo aumentaria divergências em relação à identidade já documentada.

### 5. Atualizar dados do produto com JavaScript pequeno

Renderizar metadados das opções com escape Razor e usar um script local carregado pela própria view, com `defer`, para atualizar categoria, unidade e preço ao selecionar produto, inclusive após erro de validação. Atualizar texto com `textContent` e tratar cor somente como valor validado. Não introduzir fetch nem mudar o layout para suportar uma seção global de scripts. Sem JavaScript, deixar informações suficientes no texto das opções para selecionar e enviar o formulário.

### 6. Verificação proporcional

Executar `dotnet build` e validar no navegador o fluxo completo, os cálculos, as falhas e os layouts. Usar duas listas para verificar isolamento. Conferir quantidade fracionária com o formato emitido pelo input e o binding do servidor, pois o projeto não configura explicitamente localização de requisições. Se necessário, ajustar apenas o tratamento do campo do módulo. Não introduzir infraestrutura de testes abrangente para esta entrega visual.

## Risks / Trade-offs

- Mapeamento de records e nomes divergentes → mapear construtores explicitamente e exercitar listagem, cadastro com erro e confirmação.
- Quantidade decimal interpretada incorretamente → verificar envio de 1,50/1.50 conforme o comportamento do input no navegador e garantir valor final de 1,5, nunca 15 ou 150.
- Estilos compartilhados afetarem outras páginas → limitar novos seletores ao módulo e revisar Minhas listas e cadastro de produtos.
- Persistência de testes atingir dados pessoais → executar verificação com dados descartáveis em perfil/local de dados isolado, sem sobrescrever o `dados.json` de uso real.
- Regras existentes apresentam limitações fora do escopo, como ausência de bloqueio para listas concluídas → preservar o comportamento atual, sem acrescentar restrições silenciosas.

## Migration Plan

Não há migração de dados. Entregar as views, conexões MVC e assets juntos. Validar compilação e cenários antes da entrega. Reverter somente os arquivos desta implementação caso necessário; o JSON continua compatível e itens criados permanecem armazenados. Preservar alterações locais anteriores durante implementação e eventual reversão.

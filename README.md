## Sobre

Este é um cadastro simples de caminhões


## Campos disponíveis

Os campos disponíveis são:

1. Id -> Id do caminhão cadastrado
2. Model -> Modelo do caminhão *
3. ManufacturingYear -> Ano de fabricação
4. ModelYear -> Ano do modelo

*Os modelos disponíveis são: 
1. 0 -> FH
2. 1 -> FM


## Endpoints
Os endpoint disponíveis são:

1. GET: /trucks/{id} -> Recupera o caminhão pelo Id

2. GET: /trucks/ -> Recupera todos os caminhões cadastrados

3. PUT: /trucks/ -> Atualiza o caminhão do Id informado no body

4. POST: /trucks -> Insere o caminhão informado no body


## Observações

* Os endpoints também podem ser acessados pelo Swagger. Basta adicionar “/swagger/index.html” ao final da url. Ex.: https://localhost:44361/swagger/index.html

* No recurso de inserção (POST), caso seja informado um caminhão válido, o id do mesmo é retornado, facilitando possíveis consultas

* Ao informar dados inválidos, uma ou mais mensagens de validações são retornadas
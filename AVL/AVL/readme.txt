README - Projeto AVL em C#
Arquivos:
- Program.cs: Código-fonte principal
- entrada.txt: Contém os testes do exemplo 1 do PDF
- entrada2.txt: Contém os testes do exemplo 2 do PDF

Todos os comandos do enunciado são suportados:
I valor  - Inserção
R valor  - Remoção
B valor  - Busca
P        - Impressão em pré-ordem
F        - Fatores de balanceamento
H        - Altura da árvore

A tentativa de inserir valores repetidos gera a mensagem: "Valor já existente".

Durante os testes com os comandos do PDF, principalmente os de inserção (como "I 10", "I 20", "I 30"), a saída gerada pode parecer diferente da que está no exemplo do enunciado.

No PDF, por exemplo, aparece a linha:
Árvore em pré-ordem: 10 20 30

Mas o programa pode exibir:
Árvore em pré-ordem: 20 10 30

Isso acontece porque a árvore AVL se ajusta automaticamente quando fica desbalanceada, fazendo rotações para manter o equilíbrio. No caso da sequência acima, a estrutura original causaria um desbalanceamento e a árvore corrige isso com uma rotação, o que é exatamente o comportamento esperado de uma AVL.

Mesmo que a ordem dos números pareça diferente, a lógica está funcionando corretamente. Essa diferença é normal e acontece porque o algoritmo está aplicando os balanceamentos exigidos pela estrutura AVL.

Essa observação foi incluída só pra evitar confusão na hora da correção.



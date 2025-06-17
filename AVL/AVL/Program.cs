
using System;
using System.Collections.Generic;
using System.IO;

namespace TrabalhoAVL
{
    class Node
    {
        public int Valor;
        public Node Esquerda;
        public Node Direita;
        public int Altura;

        public Node(int valor)
        {
            Valor = valor;
            Altura = 1;
        }
    }

    class ArvoreAVL
    {
        private Node raiz;

        public void Inserir(int valor)
        {
            raiz = Inserir(raiz, valor);
        }

        public void Remover(int valor)
        {
            raiz = Remover(raiz, valor);
        }

        public void Buscar(int valor)
        {
            bool encontrado = Buscar(raiz, valor);
            Console.WriteLine(encontrado ? "Valor encontrado" : "Valor não encontrado");
        }

        public void PreOrdem()
        {
            Console.Write("Árvore em pré-ordem: ");
            PreOrdem(raiz);
            Console.WriteLine();
        }

        public void FatoresBalanceamento()
        {
            Console.WriteLine("Fatores de balanceamento:");
            FatoresBalanceamento(raiz);
        }

        public void Altura()
        {
            Console.WriteLine($"Altura da árvore: {Altura(raiz)}");
        }

        private Node Inserir(Node node, int valor)
        {
            if (node == null) return new Node(valor);

            if (valor < node.Valor) node.Esquerda = Inserir(node.Esquerda, valor);
            else if (valor > node.Valor) node.Direita = Inserir(node.Direita, valor);
            else
            {
                Console.WriteLine("Valor já existente");
                return node;
            }

            AtualizarAltura(node);
            return Balancear(node);
        }

        private Node Remover(Node node, int valor)
        {
            if (node == null) return null;

            if (valor < node.Valor) node.Esquerda = Remover(node.Esquerda, valor);
            else if (valor > node.Valor) node.Direita = Remover(node.Direita, valor);
            else
            {
                if (node.Esquerda == null || node.Direita == null)
                {
                    node = node.Esquerda ?? node.Direita;
                }
                else
                {
                    Node sucessor = GetMinimo(node.Direita);
                    node.Valor = sucessor.Valor;
                    node.Direita = Remover(node.Direita, sucessor.Valor);
                }
            }

            if (node == null) return null;

            AtualizarAltura(node);
            return Balancear(node);
        }

        private Node GetMinimo(Node node)
        {
            while (node.Esquerda != null) node = node.Esquerda;
            return node;
        }

        private bool Buscar(Node node, int valor)
        {
            if (node == null) return false;
            if (valor == node.Valor) return true;
            return valor < node.Valor ? Buscar(node.Esquerda, valor) : Buscar(node.Direita, valor);
        }

        private void PreOrdem(Node node)
        {
            if (node != null)
            {
                Console.Write($"{node.Valor} ");
                PreOrdem(node.Esquerda);
                PreOrdem(node.Direita);
            }
        }

        private void FatoresBalanceamento(Node node)
        {
            if (node != null)
            {
                Console.WriteLine($"Nó {node.Valor}: Fator de balanceamento {FatorBalanceamento(node)}");
                FatoresBalanceamento(node.Esquerda);
                FatoresBalanceamento(node.Direita);
            }
        }

        private int Altura(Node node) => node?.Altura ?? 0;

        private void AtualizarAltura(Node node)
        {
            node.Altura = Math.Max(Altura(node.Esquerda), Altura(node.Direita)) + 1;
        }

        private int FatorBalanceamento(Node node)
        {
            return Altura(node.Esquerda) - Altura(node.Direita);
        }

        private Node Balancear(Node node)
        {
            int fb = FatorBalanceamento(node);
            if (fb > 1)
            {
                if (FatorBalanceamento(node.Esquerda) < 0)
                    node.Esquerda = RotacionarEsquerda(node.Esquerda);
                return RotacionarDireita(node);
            }
            else if (fb < -1)
            {
                if (FatorBalanceamento(node.Direita) > 0)
                    node.Direita = RotacionarDireita(node.Direita);
                return RotacionarEsquerda(node);
            }
            return node;
        }

        private Node RotacionarDireita(Node y)
        {
            Node x = y.Esquerda;
            Node T2 = x.Direita;
            x.Direita = y;
            y.Esquerda = T2;
            AtualizarAltura(y);
            AtualizarAltura(x);
            return x;
        }

        private Node RotacionarEsquerda(Node x)
        {
            Node y = x.Direita;
            Node T2 = y.Esquerda;
            y.Esquerda = x;
            x.Direita = T2;
            AtualizarAltura(x);
            AtualizarAltura(y);
            return y;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ArvoreAVL arvore = new ArvoreAVL();
            string[] linhas = File.ReadAllLines("entrada.txt");

            foreach (string linha in linhas)
            {
                if (string.IsNullOrWhiteSpace(linha)) continue;
                string[] partes = linha.Split(' ');
                string comando = partes[0];
                int valor = partes.Length > 1 ? int.Parse(partes[1]) : 0;

                switch (comando)
                {
                    case "I": arvore.Inserir(valor); break;
                    case "R": arvore.Remover(valor); break;
                    case "B": arvore.Buscar(valor); break;
                    case "P": arvore.PreOrdem(); break;
                    case "F": arvore.FatoresBalanceamento(); break;
                    case "H": arvore.Altura(); break;
                }
            }
        }
    }
}

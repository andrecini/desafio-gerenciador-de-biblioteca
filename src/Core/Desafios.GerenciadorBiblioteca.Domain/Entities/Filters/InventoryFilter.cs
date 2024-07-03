﻿using Desafios.GerenciadorBiblioteca.Domain.Enums;

namespace Desafios.GerenciadorBiblioteca.Domain.Entities.Filters
{
    public class InventoryFilter
    {
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public InventoryStatus Status { get; set; }
    }
}

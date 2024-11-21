using System;

namespace LibraryManagement.Models
{
    public class BorrowRecord
    {
        public int BorrowRecordId { get; set; }
        public int BookId { get; set; }
        public string BorrowerName { get; set; }
        public string BorrowerEmail { get; set; }
        public string Phone { get; set; }
        public DateTime BorrowDate { get; set; }

        // Set due date as a specific period (e.g., 14 days after the borrow date)
        public DateTime DueDate => BorrowDate.AddDays(14);

        public DateTime? ReturnDate { get; set; }

        public virtual Book Book { get; set; } // Navigation property
        public bool IsOverdue => ReturnDate == null && DateTime.UtcNow > DueDate;
    }
}

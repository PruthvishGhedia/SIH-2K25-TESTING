namespace SIH.ERP.Soap.Models;

/// <summary>
/// Represents a library item (book or resource) in the educational institution.
/// This model stores information about library resources, including title, author, and availability.
/// </summary>
public class Library
{
    /// <summary>
    /// Unique identifier for the library item.
    /// This is an auto-generated primary key assigned by the system.
    /// </summary>
    /// <example>1</example>
    public int book_id { get; set; }
    
    /// <summary>
    /// Title of the library item.
    /// Used for identification and searching within the library catalog.
    /// </summary>
    /// <example>Introduction to Computer Science</example>
    public string? title { get; set; }
    
    /// <summary>
    /// Author or creator of the library item.
    /// Used for identification and searching within the library catalog.
    /// </summary>
    /// <example>John Doe</example>
    public string? author { get; set; }
    
    /// <summary>
    /// Shelf location or section where the item is stored.
    /// Used for physical location and retrieval of library items.
    /// </summary>
    /// <example>CS-A1</example>
    public string? shelf { get; set; }
    
    /// <summary>
    /// International Standard Book Number (ISBN) for the item.
    /// Used for unique identification and ordering of library items.
    /// </summary>
    /// <example>978-1234567890</example>
    public string? isbn { get; set; }
    
    /// <summary>
    /// Number of copies available in the library.
    /// Used to track availability and lending capacity.
    /// </summary>
    /// <example>5</example>
    public int? copies { get; set; }
}
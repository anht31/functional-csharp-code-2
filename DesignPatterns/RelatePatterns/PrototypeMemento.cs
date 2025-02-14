
using System;
using System.Collections.Generic;
namespace DesignPatterns.RelatePatterns.PrototypeMemento;

// Giao diện Prototype cho phép clone đối tượng
public interface IPrototype<T>
{
    T Clone();
}

// Lớp Document đại diện cho trạng thái cần lưu
public class Document : IPrototype<Document>
{
    public string Title { get; set; }
    public string Content { get; set; }

    public Document(string title, string content)
    {
        Title = title;
        Content = content;
    }

    // Phương thức Clone thực hiện deep copy (với chuỗi, copy trực tiếp đã đủ vì chuỗi là immutable)
    public Document Clone()
    {
        return new Document(this.Title, this.Content);
    }

    public override string ToString()
    {
        return $"Document Title: {Title}\nContent: {Content}";
    }
}

// Lớp DocumentHistory hoạt động như Caretaker, lưu lại các snapshot của Document
public class DocumentHistory
{
    private readonly List<Document> _history = new List<Document>();

    public void Save(Document doc)
    {
        // Lưu lại snapshot bằng cách clone Document
        _history.Add(doc.Clone());
    }

    public Document Restore(int index)
    {
        // Trả về bản sao của snapshot để tránh bị thay đổi từ bên ngoài
        return _history[index].Clone();
    }
}

class Client
{
    public void Run()
    {
        // Tạo một Document ban đầu
        Document doc = new Document("Version 1", "Initial content");
        DocumentHistory history = new DocumentHistory();

        // Lưu trạng thái ban đầu
        history.Save(doc);
        Console.WriteLine("Initial document state:");
        Console.WriteLine(doc);

        // Thay đổi Document và lưu lại trạng thái mới
        doc.Content = "Updated content after first edit.";
        history.Save(doc);
        Console.WriteLine("\nAfter first update:");
        Console.WriteLine(doc);

        // Thay đổi Document thêm lần nữa (không lưu lại trạng thái)
        doc.Content = "Updated content after second edit.";
        Console.WriteLine("\nAfter second update:");
        Console.WriteLine(doc);

        // Khôi phục Document về trạng thái ban đầu
        Document restoredDoc = history.Restore(0);
        Console.WriteLine("\nRestored to initial state:");
        Console.WriteLine(restoredDoc);
    }
}
using LibeyTechnicalTestDomain.EFCore;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.DTO;
using LibeyTechnicalTestDomain.LibeyUserAggregate.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibeyTechnicalTestDomain.LibeyUserAggregate.Infrastructure
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly Context _context;

        public DocumentTypeRepository(Context context)
        {
            _context = context;
        }

        public List<DocumentTypeResponse> FindAllResponses()
        {
            return _context.DocumentType
                .Select(documentType => new DocumentTypeResponse
                {
                    DocumentTypeId = documentType.DocumentTypeId,
                    DocumentTypeDescription = documentType.DocumentTypeDescription
                }).ToList();
        }
    }
}

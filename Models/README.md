# DEVELOPMENT

dotnet ef dbcontext scaffold "Server=localhost;Database=greenleaves;User=root;Password=;TreatTinyAsBoolean=true;" "Pomelo.EntityFrameworkCore.MySql" --output-dir Domain --context-dir ./ --context ApplicationDbContext --force
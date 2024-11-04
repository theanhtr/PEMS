namespace Predict.Model
{
    public class PestStage
    {
        public Guid PestStageId { get; set; }
        public Guid? PestId { get; set; } // Foreign key to Pest
        public string? PestStageName { get; set; }
        public double? T0 { get; set; } // Nhiệt độ phát dục (t0)
        public double? K1 { get; set; } // K từng giai đoạn (K1)
        public double? K { get; set; } // Tổng tích ôn hữu hiệu (K)
    }

    public class Pest
    {
        public Guid PestId { get; set; }
        public string PestName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }

    public class LevelWarning
    {
        public Guid LevelWarningId { get; set; }
        public string LevelWarningName { get; set; }
        public Guid? CropStageId { get; set; } // Foreign key to CropStage
        public Guid? PestStageId { get; set; } // Foreign key to PestStage
        public Guid? PestId { get; set; } // Foreign key to Pest
        public Guid? CropId { get; set; } // Foreign key to Crop
    }

    public class CropStage
    {
        public Guid CropStageId { get; set; }
        public Guid? CropId { get; set; } // Foreign key to Crop
        public string CropStageName { get; set; } // Giai đoạn cây trồng
    }

    public class Crop
    {
        public Guid CropId { get; set; }
        public string CropName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}

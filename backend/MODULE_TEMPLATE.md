# 模块模板

下面这套结构可以直接复制，用来快速创建一个新模块。

## 推荐文件清单

以 `Product` 模块为例：

```text
GoPractice.Domain/
  Entities/
    Product.cs

GoPractice.Application/
  Dtos/
    Product/
      ProductDto.cs
      ProductCreateRequest.cs
      ProductUpdateRequest.cs
  Interfaces/
    IProductRepository.cs
    IProductService.cs
  Services/
    ProductService.cs

GoPractice.Infrastructure/
  Repositories/
    ProductRepository.cs

GoPractice.Api/
  Controllers/
    ProductController.cs
```

## 推荐继承关系

- `ProductService` 继承 `CrudAppService<TEntity, TDto, TCreateRequest, TUpdateRequest>`
- `ProductRepository` 继承 `SqlSugarRepository<TEntity>`
- `ProductController` 继承 `BaseApiController`

## 最小步骤

1. 新建实体并继承 `BaseEntity`
2. 新建 `Dto / CreateRequest / UpdateRequest`
3. 新建 `IProductRepository` 继承 `IRepository<Product>`
4. 新建 `IProductService` 继承 `ICrudAppService<...>`
5. 新建 `ProductRepository`，只有需要自定义查询时再重写分页逻辑
6. 新建 `ProductService`，只重写：
   - `ValidateCreateAsync`
   - `ValidateUpdateAsync`
   - `MapToDto`
   - `MapCreateRequest`
   - `MapUpdateRequest`
7. 新建 `ProductController`，按 `DemoController` 的方式暴露接口
8. 在依赖注入中注册：
   - `IProductRepository -> ProductRepository`
   - `IProductService -> ProductService`

## 参考实现

- 示例服务：[DemoService.cs](file:///c:/Workplace/GOPRACTICE/go-practice/backend/GoPractice.Application/Services/DemoService.cs)
- 示例仓储：[DemoRepository.cs](file:///c:/Workplace/GOPRACTICE/go-practice/backend/GoPractice.Infrastructure/Repositories/DemoRepository.cs)
- 示例控制器：[DemoController.cs](file:///c:/Workplace/GOPRACTICE/go-practice/backend/GoPractice.Api/Controllers/DemoController.cs)
- 通用 CRUD 基类：[CrudAppService.cs](file:///c:/Workplace/GOPRACTICE/go-practice/backend/GoPractice.Application/Services/CrudAppService.cs)
- 通用仓储基类：[SqlSugarRepository.cs](file:///c:/Workplace/GOPRACTICE/go-practice/backend/GoPractice.Infrastructure/Repositories/SqlSugarRepository.cs)

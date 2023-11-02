using Hangfire;
using Hangfire.Common;
using Microsoft.AspNetCore.Mvc;
using MaterialsStockMarket.Entities.Material;
using MaterialsStockMarket.Entities.Material.Commands;
using MaterialsStockMarket.Entities.Material.Queries;
using MaterialsStockMarket.Entities.Seller;
using MaterialsStockMarket.Entities.Seller.Commands;
using MaterialsStockMarket.Entities.Seller.Queries;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;


namespace MaterialsStockMarket.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StockMarketController : ControllerBase
    {
        //Класс посредник, реализующий интерфейс IMediator, для обработки запросов к бд
        private readonly IMediator _mediator;
        
        //Классы проверки валидности данных
        private readonly MaterialValidator _materialValidator;
        private readonly SellerValidator _sellerValidator;
        
        public StockMarketController(IMediator mediator)
        {
            _mediator = mediator;
            _materialValidator = new ();
            _sellerValidator = new ();
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Получение списка всех материалов")]
        public async Task<IActionResult> GetMaterials()
        {
            //Обработка запроса
            List<Material> result = await _mediator.Send(new GetMaterialsQuery());

            //Возвращение списка материалов с кодом 200
            return Ok(result);
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Получение информации о материале по его Id")]
        public async Task<IActionResult> GetMaterialById(int id)
        {
            //Обработка запроса
            var result = await _mediator.Send(new GetMaterialByIdQuery(){Id = id });
        
            //Если найти материал не удалось - возвращение кода 404
            if (result == null)
                return NotFound("Материал с таким id не найден");
        
            //Возвращение материала с кодом 200
            return Ok(result);
        }
        
        [HttpPost]
        [SwaggerOperation(Summary = "Создание нового материала с указанием продавца, автогенерация id материала при id = 0")]
        public async Task<IActionResult> AddNewMaterial(CreateMaterialCommand command)
        {
            //Проверка данных на валидность
            var validationResult = await _materialValidator.ValidateAsync(command.Material);

            //Возвращение кода 400, если значения не прошли проверку 
            if (!validationResult.IsValid)
                return BadRequest("Переданные значения не валидны");

            //Проверка существования id продавца 
            var checkSellerId = await _mediator.Send(new CheckSellerIdQuery() { Id = command.Material.SellerId });

            //Возвращение кода 400, если id продавца не найден
            if (!checkSellerId)
                return BadRequest("Продавец с таким id не найден");
            
            //Обработка запроса
            var result = await _mediator.Send(command);

            //Возвращение кода 400, если материал с таким id существует
            if (result == null)
                return BadRequest("Материал с таким id уже существует");
            
            //Возвращение кода 200 и добавленный материал
            return Ok(result);
        }
        
        [HttpPut]
        [SwaggerOperation(Summary = "Обновление информации о материале")]
        public async Task<IActionResult> UpdateMaterial(UpdateMaterialCommand command)
        {
            //Проверка данных на валидность
            var validationResult = await _materialValidator.ValidateAsync(command.Material);

            //Возвращение кода 400, если значения не прошли проверку 
            if (!validationResult.IsValid)
                return BadRequest("Переданные значения не валидны");
            
            //Проверка существования продавца с таким id
            var checkSellerId = await _mediator.Send(new CheckSellerIdQuery() { Id = command.Material.SellerId });

            //возвращение кода 400, если id продавца не найден
            if (!checkSellerId)
                return BadRequest("Продавец с таким id не найден");
            
            //Обработка запроса
            var result = await _mediator.Send(command);
            
            //Если найти материал не удалось - возвращение кода 400
            if (result == null)
                return NotFound("Найти и обновить материал с таким id не удалось");
            
            //При успешном обновлении - возвращение кода 200 и обновленный материал
            return Ok(result);
        }
        
        [HttpDelete]
        [SwaggerOperation(Summary = "Удаление материала")]
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            //Обработка запроса
            var result = await _mediator.Send(new DeleteMaterialCommand(){Id = id});

            //Если найти материал не удалось - возвращение кода 400
            if (!result)
                return NotFound("Найти и удалить материал с таким id не удалось");

            //При успешном удалении - возвращение кода 400
            return Ok();
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Получение списка всех продавцов")]
        public async Task<IActionResult> GetAllSellers()
        {
            //Обработка запроса
            var result = await _mediator.Send(new GetSellersQuery());
        
            //Возвращение списка продавцов с кодом 200
            return Ok(result);
        }
        
        [HttpGet]
        [SwaggerOperation(Summary = "Получение информации о продавце по его Id")]
        public async Task<IActionResult> GetSellerById(int id)
        {
            //Обработка запроса
            var result = await _mediator.Send(new GetSellerByIdQuery(){ Id = id});
        
            //Если найти продавца не удалось - возвращение кода 404
            if (result == null)
                return NotFound("Продавец с таким id не найден");
        
            //Возвращение продавца с кодом 200
            return Ok(result);
        }
        
        [HttpPost]
        [SwaggerOperation(Summary = "Создание нового продавца, автогенерация id продавца при id = 0")]
        public async Task<IActionResult> CreateSeller([FromBody]CreateSellerCommand command)
        {
            //Проверка данных на валидность
            var validationResult = await _sellerValidator.ValidateAsync(command.Seller);

            //Возвращение кода 400, если значения не прошли проверку
            if (!validationResult.IsValid)
                return BadRequest("Переданные значения не валидны");

            //Обработка запроса
            var result = await _mediator.Send(command);
            
            //Возвращение кода 400, если продавец с таким id существует
            if (result == null)
                return BadRequest("Продавец с таким id уже существует");
            
            //Возвращение кода 200 и добавленного продавца
            return Ok(result);
        }
        
        [HttpPut]
        [SwaggerOperation(Summary = "Обновление информации о продавце")]
        public async Task<IActionResult> UpdateSeller(UpdateSellerCommand command)
        {
            //Проверка данных на валидность
            var validationResult = await _sellerValidator.ValidateAsync(command.Seller);

            //Возвращение кода 400, если значения не прошли проверку
            if (!validationResult.IsValid)
                return BadRequest("Переданные значения не валидны");
            
            //Обработка запроса
            var result = await _mediator.Send(command);
            
            //Если найти продавца не удалось - возвращение кода 400
            if (result == null)
                return NotFound("Найти и обновить продавца с таким id не удалось");
            
            //При успешном обновлении - возвращение кода 200 и обновленного продавца
            return Ok(result);
        }
        
        [HttpDelete]
        [SwaggerOperation(Summary = "Удаление продавца")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            //Обработка запроса
            var result = await _mediator.Send(new DeleteSellerCommand() {Id = id});
            
            //Если найти продавца не удалось - возвращение кода 400
            if (!result)
                return NotFound("Найти и удалить продавца не удалось");

            //При успешном удалении - возвращение кода 400
            return Ok();
        }
        
    }
}


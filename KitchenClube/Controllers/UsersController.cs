using KitchenClube.Data;
using KitchenClube.Exceptions;
using KitchenClube.Requests.User;
using KitchenClube.Responses;

namespace KitchenClube.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly KitchenClubContext _context;

        public UsersController(KitchenClubContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            return await _context.Users
                .Select(u => new UserResponse(u.Id, u.FullName, u.PhoneNumber, u.Email, u.IsActive))
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                throw new NotFoundException(nameof(User), id);

            return new UserResponse(user.Id, user.FullName, user.PhoneNumber, user.Email, user.IsActive);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UpdateUser updateUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user is null)
                throw new NotFoundException(nameof(User),id);
            
            user.FullName = updateUser.FullName;
            user.PhoneNumber = updateUser.PhoneNumber;
            user.IsActive = updateUser.IsActive;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<UserResponse>> PostUser(CreateUser createUser)
        {
            var email = createUser.Email.ToLower();
            if (_context.Users.Any(u => u.Email.ToLower() == email))
                throw new BadRequestException("User with this email is already regitered");

            var user = new User();
            user.FullName = createUser.FullName;
            user.Email = createUser.Email;
            user.IsActive = true;
            user.PhoneNumber = createUser.PhoneNumber;
            user.PasswordHash = createUser.Password;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new NotFoundException(nameof(user),id);

            if (_context.UserMenuItemSelections.Any(u => u.UserId == id))
                throw new BadRequestException("User can not be deleted because he/she has made menu selections");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
